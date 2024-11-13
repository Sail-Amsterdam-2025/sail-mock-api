using Microsoft.AspNetCore.Mvc;
using Sail_MockApi.Api.Services;

namespace Sail_MockApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformationController
    {
        private InformationService _informationService;

        private InformationController()
        {
            _informationService = new InformationService();
        }
        
        [HttpPost]

    }
}
