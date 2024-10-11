using Microsoft.AspNetCore.Mvc;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;

namespace OnlineStoreAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _companyServices;

        public CompanyController(ICompanyServices companyServices) 
        {
            _companyServices = companyServices;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO<CompanyShortDTO>>> CreateCompany(CompanyShortDTO data) 
        {
            ResponseDTO<CompanyShortDTO> result = new ResponseDTO<CompanyShortDTO>(null);
            try
            {
                result = await _companyServices.CreateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<CompanyDTO>>> GetCompany(string id)
        {
            ResponseDTO<CompanyDTO> result = new ResponseDTO<CompanyDTO>(null);
            try
            {
                result = await _companyServices.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyShortDTO>>> GetCompany()
        {
            ResponseDTO<IEnumerable<CompanyShortDTO>> result = new ResponseDTO<IEnumerable<CompanyShortDTO>>(null);
            try
            {
                result = await _companyServices.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<CompanyDTO>>> DeleteCompany(string id)
        {
            ResponseDTO<CompanyDTO> result = new ResponseDTO<CompanyDTO>(null);
            try
            {
                result = await _companyServices.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO<CompanyDTO>>> UpdateCompany(CompanyShortDTO data)
        {
            ResponseDTO<CompanyDTO> result = new ResponseDTO<CompanyDTO>(null);
            try
            {
                result = await _companyServices.UpdateAsync(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(result);
            }
        }
    }
}
