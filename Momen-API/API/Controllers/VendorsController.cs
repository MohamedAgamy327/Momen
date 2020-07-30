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
        public VendorsController(IMapper mapper, IUnitOfWork unitOfWork, IVendorRepository vendorRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorRepository = vendorRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Post(VendorForAddDTO model)
        {
            if (await _vendorRepository.IsExist(model.Name,model.CategoryId).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            Vendor vendor = _mapper.Map<Vendor>(model);
            await _vendorRepository.AddAsync(vendor).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/licensefile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadLicenseFile([FromForm] VendorForLicenseDTO model)
        {
            FileOperations.WriteFile("Vendor/License", model.Id, model.File);
            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);
            vendor.LicenseFileName = model.File.FileName;
            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/logofile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadLogoFile([FromForm] VendorForLogoDTO model)
        {
            FileOperations.WriteFile("Vendor/Logo", model.Id, model.File);
            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);
            vendor.LogoFileName = model.File.FileName;
            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpPatch("{id:int}/personalidfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadPersonalIdFile([FromForm] VendorForPersonalIdDTO model)
        {
            FileOperations.WriteFile("Vendor/PersonalId", model.Id, model.File);
            Vendor vendor = await _vendorRepository.GetAsync(model.Id).ConfigureAwait(true);
            vendor.PersonalIdFileName = model.File.FileName;
            _vendorRepository.Edit(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
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
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist(id)));

            if (await _vendorRepository.IsExist(model.Id, model.Name,model.CategoryId).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Delete(int id)
        {
            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist(id)));

            Vendor vendor = await _vendorRepository.GetAsync(id).ConfigureAwait(true);
            _vendorRepository.Remove(vendor);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            FolderOperations.DeleteFolder("Vendor/License", id);
            FolderOperations.DeleteFolder("Vendor/Logo", id);
            FolderOperations.DeleteFolder("Vendor/PersonalId", id);
            VendorForGetDTO vendorDto = _mapper.Map<VendorForGetDTO>(vendor);
            return Ok(vendorDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorForGetDTO>> Get(int id)
        {
            if (!await _vendorRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist(id)));

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