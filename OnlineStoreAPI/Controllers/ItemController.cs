using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemServices _itemServices;

        public ItemController(IItemServices itemServices) 
        {
            _itemServices = itemServices;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> CreateItem(ItemAddDTO data)
        {
            ResponseDTO<ItemDTO> result = new ResponseDTO<ItemDTO>(null);
            try
            {
                result = await _itemServices.CreateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ItemShortDTO>>>> GetItems(string sortBy = "", string orderType = "")
        {
            ResponseDTO<IEnumerable<ItemShortDTO>> result = new ResponseDTO<IEnumerable<ItemShortDTO>>(null);
            try
            {
                result = await _itemServices.GetAsync(sortBy, orderType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ItemShortDTO>>>> GetItemSearchArguments(ItemSearchArguments searchArguments, string sortBy = "", string orderType = "")
        {
            ResponseDTO<IEnumerable<ItemShortDTO>> result = new ResponseDTO<IEnumerable<ItemShortDTO>>(null);
            try
            {
                result = await _itemServices.GetItemSearchArgumentsAsync(searchArguments, sortBy, orderType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>>> GetItemPriceHistory(string itemId)
        {
            ResponseDTO<IEnumerable<ItemPriceHistoryDTO>> result = new ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>(null);
            try
            {
                result = await _itemServices.GetItemPriceHistoryAsync(itemId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{itemCategoryId}")]
        public async Task<ActionResult<ResponseDTO<IEnumerable<PropertyValuesDistinct>>>> GetDistinctValuesAsync(string itemCategoryId)
        {
            ResponseDTO<PropertyValuesDistinct> result = new ResponseDTO<PropertyValuesDistinct>(null);
            try
            {
                result = await _itemServices.GetDistinctValuesAsync(itemCategoryId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> GetItem(string id)
        {
            ResponseDTO<ItemDTO> result = new ResponseDTO<ItemDTO>(null);
            try
            {
                result = await _itemServices.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> UpdateItem(ItemAddDTO data)
        {
            ResponseDTO<ItemDTO> result = new ResponseDTO<ItemDTO>(null);
            try
            {
                result = await _itemServices.UpdateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> DeleteItem(string id)
        {
            ResponseDTO<ItemDTO> result = new ResponseDTO<ItemDTO>(null);
            try
            {
                result = await _itemServices.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
    }
}
