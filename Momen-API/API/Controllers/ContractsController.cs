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
using Utilities.StaticHelpers;

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
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            Contract contract = _mapper.Map<Contract>(model);

            await _contractRepository.AddAsync(contract).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Put(int id, ContractForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Contract", id)));

            if (await _contractRepository.IsExist(model.Id, model.Name).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            Contract oldContract = await _contractRepository.GetAsync(id).ConfigureAwait(true);
            Contract contract = _mapper.Map<Contract>(model);

            contract.FileName = oldContract.FileName;
            _contractRepository.Edit(contract);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpPatch("{id:int}/file")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UploadFile(int id, [FromForm] ContractForFileDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Contract", id)));

            FileOperations.WriteFile($"Contract/{model.Id}", model.File);

            Contract contract = await _contractRepository.GetAsync(model.Id).ConfigureAwait(true);

            contract.FileName = model.File.FileName;
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
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Contract", id)));

            Contract contract = await _contractRepository.GetAsync(id).ConfigureAwait(true);

            _contractRepository.Remove(contract);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            FolderOperations.DeleteFolder($"Contract/{id}");

            ContractForGetDTO contractDto = _mapper.Map<ContractForGetDTO>(contract);
            return Ok(contractDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ContractForGetDTO>> Get(int id)
        {
            if (!await _contractRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Contract", id)));

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