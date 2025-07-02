using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BakeryAPI.Services;
using System.Threading.Tasks;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorService _distributorService;

        public DistributorsController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var distributors = await _distributorService.GetAllDistributorsAsync();
            return Ok(distributors);
        }
    }
}
