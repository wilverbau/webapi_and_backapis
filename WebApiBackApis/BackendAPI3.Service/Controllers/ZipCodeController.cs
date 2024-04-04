using BackendAPI3.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI3.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZipCodeController : ControllerBase
    {
        private readonly IZipCodesService _zipCodesService;

        private readonly ILogger<ZipCodeController> _logger;

        public ZipCodeController(IZipCodesService zipCodesService, ILogger<ZipCodeController> logger)
        {
            _zipCodesService = zipCodesService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ZipCode> Get()
        {
            return _zipCodesService.GetAll();
        }

        [HttpGet("{zipcode}")]
        public ActionResult Get(int zipcode)
        {
            Task.Delay(2000).Wait();
            var zipCode = _zipCodesService.GetByZip(zipcode);
            if (zipCode == null)
                return NotFound();
            return Ok(zipCode);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ZipCode zipCode)
        {
            var add = _zipCodesService.AddZipCode(zipCode);
            if (add)
            {
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
