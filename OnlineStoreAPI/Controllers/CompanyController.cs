using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> _repositoy;

        public CompanyController(IRepository<Company> repository) 
        {
            _repositoy = repository;
        }

        [HttpPost]
        public async Task<Company> Create(Company data) 
        {
            return await _repositoy.CreateAsync(data);
        }
    }
}
