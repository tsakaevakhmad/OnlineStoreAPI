using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryServices _itemCategoryServices;

        public ItemCategoryController(IItemCategoryServices itemCategoryServices)
        {
            _itemCategoryServices = itemCategoryServices;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> CreateItemCategory(ItemCategoryAdd data)
        {
            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.CreateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> GetItemCategory(int id)
        {
            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategoryDTO>>> GetItemCategory()
        {
            ResponseDTO<IEnumerable<ItemCategoryDTO>> result = new ResponseDTO<IEnumerable<ItemCategoryDTO>>(null);
            try
            {
                result = await _itemCategoryServices.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> DeleteItemCategory(int id)
        {
            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> UpdateItemCategory(int id, UpdateItemCategory data)
        {
            if (id != data.Id)
                return BadRequest("Incorrect id");

            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.UpdateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPost("{itemCategoryId}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> AddPropertyToItemCategory(int itemCategoryId, ItemCategoryAddProperties data)
        {
            if (itemCategoryId != data.ItemCategoryId)
                return BadRequest("Incorrect id");

            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.AddPropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> UpdatePropertyToItemCategory(int id, ItemCategoryAddProperties data)
        {
            if (id != data.ItemCategoryId)
                return BadRequest("Incorrect id");

            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.UpdatePropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemCategoryDTO>>> DeletePropertyToItemCategory(int id, ItemCategoryDeleteProperties data)
        {
            if (id != data.ItemCategoryId)
                return BadRequest("Incorrect id");

            ResponseDTO<ItemCategoryDTO> result = new ResponseDTO<ItemCategoryDTO>(null);
            try
            {
                result = await _itemCategoryServices.DeletePropertyAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
    }
}
