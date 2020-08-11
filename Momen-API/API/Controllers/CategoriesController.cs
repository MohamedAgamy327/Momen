using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO.Category;
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
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IVendorRepository _vendorRepository;
        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IVendorRepository vendorRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryForGetDTO>> Post(CategoryForAddDTO model)
        {
            if (await _categoryRepository.IsExist(model.Name).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            Category category = _mapper.Map<Category>(model);

            await _categoryRepository.AddAsync(category).ConfigureAwait(true);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CategoryForGetDTO categoryDto = _mapper.Map<CategoryForGetDTO>(category);
            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryForGetDTO>> Put(int id, CategoryForEditDTO model)
        {
            if (id != model.Id)
                return BadRequest(new ApiResponse(400, StringConcatenates.NotEqualIds(id, model.Id)));

            if (!await _categoryRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Category", id)));

            if (await _categoryRepository.IsExist(model.Id, model.Name).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConsts.EXISTED));

            Category category = _mapper.Map<Category>(model);

            _categoryRepository.Edit(category);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CategoryForGetDTO categoryDto = _mapper.Map<CategoryForGetDTO>(category);
            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryForGetDTO>> Delete(int id)
        {
            if (!await _categoryRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Category", id)));

            if (await _vendorRepository.IsExistByCategory(id).ConfigureAwait(true))
                return Conflict(new ApiResponse(409, StringConcatenates.Exist(id, "vendors")));

            Category category = await _categoryRepository.GetAsync(id).ConfigureAwait(true);

            _categoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync().ConfigureAwait(true);

            CategoryForGetDTO knwoningDto = _mapper.Map<CategoryForGetDTO>(category);
            return Ok(knwoningDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryForGetDTO>> Get(int id)
        {
            if (!await _categoryRepository.IsExist(id).ConfigureAwait(true))
                return NotFound(new ApiResponse(404, StringConcatenates.NotExist("Category", id)));

            Category category = await _categoryRepository.GetAsync(id).ConfigureAwait(true);
            CategoryForGetDTO categoryDto = _mapper.Map<CategoryForGetDTO>(category);
            return Ok(categoryDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<CategoryForGetDTO>>> Get()
        {
            List<CategoryForGetDTO> categorys = _mapper.Map<List<CategoryForGetDTO>>(await _categoryRepository.GetAsync().ConfigureAwait(true));
            return Ok(categorys);
        }
    }
}