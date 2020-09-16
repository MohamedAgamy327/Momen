using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.VendorReject;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.UnitOfWork;
using Core.IRepository;
using Microsoft.AspNetCore.Http;
using API.Errors;
using Utilities.StaticHelpers;
using Domain.Enums;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorRejectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorRejectRepository _vendorRejectRepository;
        private readonly IVendorRepository _vendorRepository;

        public VendorRejectsController(IMapper mapper, IUnitOfWork unitOfWork, IVendorRejectRepository vendorRejectRepository, IVendorRepository vendorRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorRejectRepository = vendorRejectRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorRejectForGetDTO>> Post(VendorRejectForAddDTO model)
        {
            if (!await _vendorRepository.IsExist(model.VendorId).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", model.VendorId)));

            VendorReject vendorReject = _mapper.Map<VendorReject>(model);

            await _vendorRejectRepository.AddAsync(vendorReject).ConfigureAwait(true);

            Vendor vendor =await _vendorRepository.GetAsync(model.VendorId).ConfigureAwait(true);

            vendor.Status = VendorStatusEnum.Rejected;
            _vendorRepository.Edit(vendor);

            await _unitOfWork.CompleteAsync().ConfigureAwait(true);



            VendorRejectForGetDTO vendorRejectDto = _mapper.Map<VendorRejectForGetDTO>(vendorReject);
            return Ok(vendorRejectDto);
        }

        [HttpGet("vendors/{vendorId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<VendorRejectForGetDTO>>> GetByVendor(int vendorId)
        {
            if (!await _vendorRepository.IsExist(vendorId).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", vendorId)));

            List<VendorRejectForGetDTO> vendorRejects = _mapper.Map<List<VendorRejectForGetDTO>>(await _vendorRejectRepository.GetByVendorAsync(vendorId).ConfigureAwait(true));
            return Ok(vendorRejects);
        }
    }
}