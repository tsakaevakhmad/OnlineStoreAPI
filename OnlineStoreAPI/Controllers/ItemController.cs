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
        public async Task<ActionResult<ResponseDTO<IEnumerable<ItemShortDTO>>>> GetItems()
        {
            ResponseDTO<IEnumerable<ItemShortDTO>> result = new ResponseDTO<IEnumerable<ItemShortDTO>>(null);
            try
            {
                result = await _itemServices.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO<IEnumerable<ItemShortDTO>>>> GetItemSearchArguments([FromQuery]ItemSearchArguments searchArguments)
        {
            ResponseDTO<IEnumerable<ItemShortDTO>> result = new ResponseDTO<IEnumerable<ItemShortDTO>>(null);
            try
            {
                result = await _itemServices.GetItemSearchArgumentsAsync(searchArguments);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> GetItem(int id)
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> UpdateItem(int id, ItemAddDTO data)
        {
            if (id != data.Id)
                return BadRequest("Incorrect id");

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
        public async Task<ActionResult<ResponseDTO<ItemDTO>>> DeleteItem(int id)
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
