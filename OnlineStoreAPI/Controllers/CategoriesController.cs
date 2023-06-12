using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryListDTO>>> GetCategory()
        {
            ResponseDTO<IEnumerable<CategoryListDTO>> result = new ResponseDTO<IEnumerable<CategoryListDTO>>(null);
            try
            {
                result = await _categoryServices.GetAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryListDTO>> GetCategory(int id)
        {
            ResponseDTO<CategoryListDTO> result = new ResponseDTO<CategoryListDTO>(null);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryListDTO>> PutCategory(int id, CategoryListDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest("Incorrect id");
            }

            ResponseDTO<CategoryListDTO> result = new ResponseDTO<CategoryListDTO>(null);
            try
            {
                result = await _categoryServices.UpdateAsync(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryListDTO>> PostCategory(CategoryListDTO category)
        {
            ResponseDTO<CategoryListDTO> result = new ResponseDTO<CategoryListDTO>(null);
            try
            {
                result = await _categoryServices.CreateAsync(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryListDTO>> DeleteCategory(int id)
        {
            ResponseDTO<CategoryListDTO> result = new ResponseDTO<CategoryListDTO>(null);
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
    }
}
