using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices itemCategoryServices)
        {
            _categoryServices = itemCategoryServices;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> CreateCategory(CategoryAdd data)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.CreateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> GetCategory(string id)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
        {
            ResponseDTO<IEnumerable<CategoryDTO>> result = new ResponseDTO<IEnumerable<CategoryDTO>>(null);
            try
            {
                result = await _categoryServices.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> DeleteCategory(string id)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> UpdateCategory(UpdateCategory data)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.UpdateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> AddPropertyToCategory(CategoryAddProperties data)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.AddPropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> UpdatePropertyToCategory(CategoryAddProperties data)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.UpdatePropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> DeletePropertyToCategory(CategoryDeleteProperties data)
        {
            ResponseDTO<CategoryDTO> result = new ResponseDTO<CategoryDTO>(null);
            try
            {
                result = await _categoryServices.DeletePropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
    }
}
