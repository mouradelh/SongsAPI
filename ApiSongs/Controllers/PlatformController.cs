using ApiSongs.Models;
using ApiSongs.Services;
using ApiSongs.View;
using Microsoft.AspNetCore.Mvc;

namespace ApiSongs.Controllers
{
    [Route("[Controller]/[action]")]
    public class PlatformController : Controller
    {
        private IPlatfromData _platformData;

        public PlatformController(IPlatfromData platformData)
        {
            _platformData = platformData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var platforms = _platformData.GetAll();
            return new ObjectResult(platforms);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var platform = _platformData.Get(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }
        [HttpPost]
        public IActionResult Create([FromBody]CreatePlatformView createPlatformView)
        {
            var newPlatform = new PlatformModel
            {
                Name = createPlatformView.Name
            };

            _platformData.Add(newPlatform);
            return CreatedAtAction(nameof(Detail), new { newPlatform.Id }, newPlatform);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var platform = _platformData.Get(id);

            if(platform == null)
            {
                return NotFound();
            }
            _platformData.Delete(platform);

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdatePlatformView updatePlatformView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var platform = _platformData.Get(id);

            if(platform == null)
            {
                return NotFound();
            }
            var updatedPlatform = new PlatformModel
            {
                Id = platform.Id,
                Name = updatePlatformView.Name
            };

            _platformData.Update(updatedPlatform);
            return NoContent();
        }
    }
}
