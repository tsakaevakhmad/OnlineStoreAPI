using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IRepository<ItemCategory> _repository;

        public ItemCategoryController(IRepository<ItemCategory> repository) 
        {
            _repository = repository;
        }

        [HttpPost]

        public async Task<ItemCategory> Create(ItemCategory data) 
        {
            return await _repository.CreateAsync(data);
        }
    }
}
