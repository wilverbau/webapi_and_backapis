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

        /// <summary>
        /// Get all zip codes
        /// </summary>
        /// <returns>The whole list of zip codes</returns>
        [HttpGet]
        public IEnumerable<ZipCode> Get()
        {
            return _zipCodesService.GetAll();
        }

        /// <summary>
        /// Retrieve the zipcode information from the zip code number.
        /// </summary>
        /// <param name="zipcode">zip code number</param>
        /// <returns>Whole zipcode information</returns>
        [HttpGet("{zipcode}")]
        public ActionResult Get(int zipcode)
        {
            var zipCode = _zipCodesService.GetByZip(zipcode);
            if (zipCode == null)
                return NotFound();
            return Ok(zipCode);
        }

        /// <summary>
        /// Adds the provided ZipCode to the data repository
        /// </summary>
        /// <param name="zipCode">a ZipCode instance</param>
        /// <returns>201 if okay, 400 otherwise</returns>
        [HttpPost]
        public ActionResult Post([FromBody] ZipCode zipCode)
        {
            var add = _zipCodesService.AddZipCode(zipCode);
            if (add)
                return Created();

            return BadRequest();

        }
    }
}
