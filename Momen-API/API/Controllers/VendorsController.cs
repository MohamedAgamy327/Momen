using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Vendor;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.UnitOfWork;
using Core.IRepository;
using Microsoft.AspNetCore.Http;
using API.Errors;
using Utilities.StaticHelpers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorRepository _vendorRepository;
        private readonly IVendorUserRepository _vendorUserRepository;
        public VendorsController(IMapper mapper, IUnitOfWork unitOfWork, IVendorRepository vendorRepository, IVendorUserRepository vendorUserRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorRepository = vendorRepository;
            _vendorUserRepository = vendorUserRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Post(VendorForAddDTO model)
        {
            if (await _vendorRepository.IsExist(model.Name, model.CategoryId).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            if (await _vendorRepository.IsExistByPhone(model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _vendorRepository.IsExistByEmail(model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            if (await _vendorUserRepository.IsExistByPhone(model.VendorUser.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.VendorUser.Phone)));

            if (await _vendorUserRepository.IsExistByEmail(model.VendorUser.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.VendorUser.Email)));

            Vendor vendor = _mapper.Map<Vendor>(model);

            VendorUser vendorUser = _mapper.Map<VendorUser>(model.VendorUser);

            string password = SecurePassword.GeneratePassword(8);

            SecurePassword.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            vendorUser.PasswordHash = passwordHash;
            vendorUser.PasswordSalt = passwordSalt;

            vendor.VendorUsers.Add(vendorUser);
            await _vendorRepository.AddAsync(vendor).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            Email.Send("Momen", model.Email, "welcome", "welcome");
            Email.Send("Momen", vendorUser.Email, "password", password);

            vendor = await _vendorRepository.GetAsync(vendor.Id).ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Put(int id, VendorForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            if (await _vendorRepository.IsExist(model.Id, model.Name, model.CategoryId).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            if (await _vendorRepository.IsExistByPhone(id, model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _vendorRepository.IsExistByEmail(id, model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            Vendor oldVendor = await _vendorRepository.GetAsync(id).ConfigureAwait(true);
            Vendor vendor = _mapper.Map<Vendor>(model);

            vendor.LicenseFileName = oldVendor.LicenseFileName;
            vendor.LogoFileName = oldVendor.LogoFileName;
            vendor.PersonalIdFileName = oldVendor.PersonalIdFileName;

            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/licensefile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadLicenseFile(int id, [FromForm] VendorForLicenseDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            FileOperations.WriteFile($"Vendors/{model.Id}/License", model.File);

            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);

            vendor.LicenseFileName = model.File.FileName;

            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/logofile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadLogoFile(int id, [FromForm] VendorForLogoDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            FileOperations.WriteFile($"Vendors/{model.Id}/Logo", model.File);
            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);
            vendor.LogoFileName = model.File.FileName;
            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/personalidfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadPersonalIdFile(int id, [FromForm] VendorForPersonalIdDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            FileOperations.WriteFile($"Vendors/{model.Id}/PersonalId", model.File);

            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);

            vendor.PersonalIdFileName = model.File.FileName;

            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Delete(int id)
        {
            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            Vendor vendor = await _vendorRepository.GetAsync(id).ConfigureAwait(true);

            _vendorRepository.Remove(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            FolderOperations.DeleteFolder($"Vendors/{id}");

            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Get(int id)
        {
            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", id)));

            Vendor vendor = await _vendorRepository.GetAsync(id).ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<VendorForGetDTO>>> Get()
        {
            List<VendorForGetDTO> vendors = _mapper.Map<List<VendorForGetDTO>>(await _vendorRepository.GetAsync().ConfigureAwait(true));
            return Ok(vendors);
        }
    }
}