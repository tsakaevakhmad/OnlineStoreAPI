using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepositories _repository;

        public ItemController(IItemRepositories repository) 
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<Item> Create(Item data)
        {
            return await _repository.CreateAsync(data);
        }

        [HttpPost]
        public async Task<ItemProperty> CreateProperty(ItemProperty data)
        {
            return await _repository.CreatePropertyAsync(data);
        }

        [HttpGet]
        public async Task<Item> Get(int id)
        {
            return await _repository.GetAsync(id);
        }
    }
}
