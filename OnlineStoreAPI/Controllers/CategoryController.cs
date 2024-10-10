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
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> GetItemCategory(int id)
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetItemCategory()
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
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> DeleteItemCategory(int id)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> UpdateItemCategory(int id, UpdateCategory data)
        {
            if (id != data.Id)
                return BadRequest("Incorrect id");

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

        [HttpPost("{itemCategoryId}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> AddPropertyToItemCategory(int itemCategoryId, CategoryAddProperties data)
        {
            if (itemCategoryId != data.CategoryId)
                return BadRequest("Incorrect id");

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

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> UpdatePropertyToItemCategory(int id, CategoryAddProperties data)
        {
            if (id != data.CategoryId)
                return BadRequest("Incorrect id");

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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<CategoryDTO>>> DeletePropertyToItemCategory(int id, CategoryDeleteProperties data)
        {
            if (id != data.CategoryId)
                return BadRequest("Incorrect id");

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
