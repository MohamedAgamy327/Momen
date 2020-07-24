using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Contract;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.UnitOfWork;
using Core.IRepository;
using Microsoft.AspNetCore.Http;
using API.Errors;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContractRepository _contractRepository;
        public ContractsController(IMapper mapper, IUnitOfWork unitOfWork, IContractRepository contractRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contractRepository = contractRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Post(ContractForAddDTO model)
        {
            if (await _contractRepository.IsExist(model.Name).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, "this resource is existed"));

            Contract contract = _mapper.Map<Contract>(model);
            await _contractRepository.AddAsync(contract).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpPatch("{{id}}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadFile([FromForm] ContractForFileDTO model)
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Content/Contract"));
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Content/Contract", model.Id.ToString()));
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Content/Contract", model.Id.ToString(), model.File.FileName);
            using FileStream fileStream = new FileStream(path, FileMode.Create);
            await model.File.CopyToAsync(fileStream).ConfigureAwait(true);
            Contract contract = await _contractRepository.GetAsync(model.Id).ConfigureAwait(true);
            contract.FileName = model.File.FileName;
            _contractRepository.Edit(contract);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Put(int id, ContractForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, $"id: {id} isn't equal model.Id: {model.Id}"));

            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, $"resource with id: {id} doesn't exist"));

            if (await _contractRepository.IsExist(model.Id, model.Name).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, "this resource is existed"));

            Contract oldContract = await _contractRepository.GetAsync(id).ConfigureAwait(true);
            Contract contract = _mapper.Map<Contract>(model);
            contract.FileName = oldContract.FileName;
            _contractRepository.Edit(contract);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Delete(int id)
        {
            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, $"resource with id: {id} doesn't exist"));

            Contract contract = await _contractRepository.GetAsync(id).ConfigureAwait(true);
            _contractRepository.Remove(contract);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            System.IO.File.Delete($"{Directory.GetCurrentDirectory()}/Content/{contract.Id}/{contract.Name}");
            ContractForGetDTO knwoningDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(knwoningDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Get(int id)
        {
            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, $"resource with id: {id} doesn't exist"));

            Contract contract = await _contractRepository.GetAsync(id).ConfigureAwait(true);
            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ContractForGetDTO>>> Get()
        {
            List<ContractForGetDTO> contracts = _mapper.Map<List<ContractForGetDTO>>(await _contractRepository.GetAsync().ConfigureAwait(true));
            return Ok(contracts);
        }
    }
}