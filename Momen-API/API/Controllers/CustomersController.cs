using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Customer;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(IMapper mapper, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerForGetDTO>> Post(CustomerForAddDTO model)
        {
            if (await _customerRepository.IsExistByPhone(model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _customerRepository.IsExistByEmail(model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            Customer customer = _mapper.Map<Customer>(model);
            string password = SecurePassword.GeneratePassword(8);

            SecurePassword.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            await _customerRepository.AddAsync(customer).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            Email.Send("Momen", customer.Email, "password", password);

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login(CustomerForLoginDTO model)
        {
            Customer customer = await _customerRepository.LoginAsync(model.Email, model.Password).ConfigureAwait(true);

            if (customer == null)
                return Unauthorized(new ApiResponse(401, StringConsts.UNAUTHORIZED));

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerForGetDTO>> Put(int id, CustomerForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            if (await _customerRepository.IsExistByPhone(id, model.Phone).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Phone", model.Phone)));

            if (await _customerRepository.IsExistByEmail(id, model.Email).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist("Email", model.Email)));

            Customer oldCustomer = await _customerRepository.GetAsync(id).ConfigureAwait(true);
            Customer customer = _mapper.Map<Customer>(model);

            customer.PasswordHash = oldCustomer.PasswordHash;
            customer.PasswordSalt = oldCustomer.PasswordSalt;
            customer.IsBlocked = oldCustomer.IsBlocked;
            customer.IsRandom = oldCustomer.IsRandom;
            customer.PictureName = oldCustomer.PictureName;

            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpPatch("{id:int}/picture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadPicture(int id, [FromForm] CustomerForPictureDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customers", id)));

            FileOperations.WriteFile($"Customers/{model.Id}", model.Picture);

            Customer customer = await _customerRepository.GetAsync(model.Id).ConfigureAwait(true);

            customer.PictureName = model.Picture.FileName;
            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpPatch("{id}/ChangePassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangePassword(int id, CustomerForChangePasswordDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(model.Id).ConfigureAwait(true);
            SecurePassword.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.IsRandom = false;

            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            return Ok();
        }

        [HttpPatch("{id}/ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ResetPassword(int id)
        {
            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(id).ConfigureAwait(true);

            string password = SecurePassword.GeneratePassword(8);
            SecurePassword.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.IsRandom = true;

            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            Email.Send("Momen", customer.Email, "password", password);

            return Ok();
        }

        [HttpPatch("{id}/Block")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Block(int id)
        {
            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(id).ConfigureAwait(true);
            customer.IsBlocked = true;

            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpPatch("{id}/Unblock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Unblock(int id)
        {
            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(id).ConfigureAwait(true);
            customer.IsBlocked = false;

            _customerRepository.Edit(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerForGetDTO>> Delete(int id)
        {
            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(id).ConfigureAwait(true);

            _customerRepository.Remove(customer);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            FolderOperations.DeleteFolder($"Customers/{id}");

            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CustomerForGetDTO>> Get(int id)
        {
            if (!await _customerRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Customer", id)));

            Customer customer = await _customerRepository.GetAsync(id).ConfigureAwait(true);
            CustomerForGetDTO customerDto = _mapper.Map<CustomerForGetDTO>(customer);
            return Ok(customerDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<CustomerForGetDTO>>> Get()
        {
            List<CustomerForGetDTO> customers = _mapper.Map<List<CustomerForGetDTO>>(await _customerRepository.GetAsync().ConfigureAwait(true));
            return Ok(customers);
        }
    }
}