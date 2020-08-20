using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.VendorPicture;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.IRepository;
using Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using API.Errors;
using Utilities.StaticHelpers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorPicturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorPictureRepository _vendorPictureRepository;
        private readonly IVendorRepository _vendorRepository;

        public VendorPicturesController(IMapper mapper, IUnitOfWork unitOfWork, IVendorPictureRepository vendorPictureRepository, IVendorRepository vendorRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vendorPictureRepository = vendorPictureRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<VendorPictureForGetDTO>>> Post([FromForm] VendorPictureForAddDTO model)
        {
            if (!await _vendorRepository.IsExist(model.VendorId).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", model.VendorId)));

            foreach (var file in model.Pictures)
            {
                FileOperations.WriteFile($"Vendors/{model.VendorId}/Pictures", file);

                VendorPicture vendorPicture = new VendorPicture
                {
                    VendorId = model.VendorId,
                    FileName = file.FileName
                };
                await _vendorPictureRepository.AddAsync(vendorPicture).ConfigureAwait(true);
            }
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            List<VendorPictureForGetDTO> vendorPictures = _mapper.Map<List<VendorPictureForGetDTO>>(await _vendorPictureRepository.GetByVendorAsync(model.VendorId).ConfigureAwait(true));
            return Ok(vendorPictures);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VendorPictureForGetDTO>> Delete(int id)
        {
            if (!await _vendorPictureRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("VendorPicture", id)));

            VendorPicture vendorPicture = await _vendorPictureRepository.GetAsync(id).ConfigureAwait(true);

            _vendorPictureRepository.Remove(vendorPicture);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);
            FileOperations.DeleteFile($"Vendors/{vendorPicture.VendorId}/Pictures", vendorPicture.FileName);

            VendorPictureForGetDTO vendorPictureDto = _mapper.Map<VendorPictureForGetDTO>(vendorPicture);
            return Ok(vendorPictureDto);
        }

        [HttpGet("vendors/{vendorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<VendorPictureForGetDTO>>> Get(int vendorId)
        {
            if (!await _vendorRepository.IsExist(vendorId).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Vendor", vendorId)));

            List<VendorPictureForGetDTO> vendorPictures = _mapper.Map<List<VendorPictureForGetDTO>>(await _vendorPictureRepository.GetByVendorAsync(vendorId).ConfigureAwait(true));
            return Ok(vendorPictures);
        }

    }
}