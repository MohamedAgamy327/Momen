using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.VendorUser;
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
    public class VendorUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorUserRepository _vendorUserRepository;
        public VendorUsersController(IMapper mapper, IUnitOfWork unitOfWork, IVendorUserRepository vendorUserRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorUserRepository = vendorUserRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorUserForGetDTO>> Post(VendorUserForAddDTO model)
        {
            if (await _vendorUserRepository.IsExistByPhone(model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _vendorUserRepository.IsExistByEmail(model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            VendorUser vendorUser = _mapper.Map<VendorUser>(model);
            string password = SecurePassword.GeneratePassword(8);

            SecurePassword.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            vendorUser.PasswordHash = passwordHash;
            vendorUser.PasswordSalt = passwordSalt;
            await _vendorUserRepository.AddAsync(vendorUser).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            Email.Send("Momen", vendorUser.Email, "password", password);
            VendorUserForGetDTO vendorUserDto = _mapper.Map<VendorUserForGetDTO>(vendorUser);
            return Ok(vendorUserDto);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login(VendorUserForLoginDTO model)
        {
            VendorUser vendorUser = await _vendorUserRepository.LoginAsync(model.Email, model.Password).ConfigureAwait(true);

            if (vendorUser == null)
                return Unauthorized(new ApiResponse(401, StringConsts.UNAUTHORIZED));

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorUserForGetDTO>> Put(int id, VendorUserForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorUserRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor User", id)));

            if (await _vendorUserRepository.IsExistByPhone(id, model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _vendorUserRepository.IsExistByEmail(id, model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            VendorUser oldVendorUser = await _vendorUserRepository.GetAsync(id).ConfigureAwait(true);
            VendorUser vendorUser = _mapper.Map<VendorUser>(model);
            vendorUser.PasswordHash = oldVendorUser.PasswordHash;
            vendorUser.PasswordSalt = oldVendorUser.PasswordSalt;
            vendorUser.VendorId = oldVendorUser.VendorId;
            vendorUser.IsRandom = oldVendorUser.IsRandom;
            _vendorUserRepository.Edit(vendorUser);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorUserForGetDTO vendorUserDto = _mapper.Map<VendorUserForGetDTO>(vendorUser);
            return Ok(vendorUserDto);
        }

        [HttpPatch("{id}/ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangePassword(int id, VendorUserForChangePasswordDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _vendorUserRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor User", id)));

            VendorUser vendorUser = await _vendorUserRepository.GetAsync(model.Id).ConfigureAwait(true);
            SecurePassword.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            vendorUser.PasswordHash = passwordHash;
            vendorUser.PasswordSalt = passwordSalt;
            vendorUser.IsRandom = false;
            _vendorUserRepository.Edit(vendorUser);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            return Ok();
        }

        [HttpPatch("{id}/ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ResetPassword(int id)
        {
            if (!await _vendorUserRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor User", id)));

            VendorUser vendorUser = await _vendorUserRepository.GetAsync(id).ConfigureAwait(true);
            string password = SecurePassword.GeneratePassword(8);
            SecurePassword.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            vendorUser.PasswordHash = passwordHash;
            vendorUser.PasswordSalt = passwordSalt;
            vendorUser.IsRandom = true;
            _vendorUserRepository.Edit(vendorUser);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            Email.Send("Momen", vendorUser.Email, "password", password);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorUserForGetDTO>> Delete(int id)
        {
            if (!await _vendorUserRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor User", id)));

            VendorUser vendorUser = await _vendorUserRepository.GetAsync(id).ConfigureAwait(true);
            _vendorUserRepository.Remove(vendorUser);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            VendorUserForGetDTO vendorUserDto = _mapper.Map<VendorUserForGetDTO>(vendorUser);
            return Ok(vendorUserDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorUserForGetDTO>> Get(int id)
        {
            if (!await _vendorUserRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor User", id)));

            VendorUser vendorUser = await _vendorUserRepository.GetAsync(id).ConfigureAwait(true);
            VendorUserForGetDTO vendorUserDto = _mapper.Map<VendorUserForGetDTO>(vendorUser);
            return Ok(vendorUserDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<VendorUserForGetDTO>>> Get()
        {
            List<VendorUserForGetDTO> vendorUsers = _mapper.Map<List<VendorUserForGetDTO>>(await _vendorUserRepository.GetAsync().ConfigureAwait(true));
            return Ok(vendorUsers);
        }
    }
}