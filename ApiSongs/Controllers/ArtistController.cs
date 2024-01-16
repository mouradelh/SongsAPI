using ApiSongs.Models;
using ApiSongs.Services;
using ApiSongs.View;
using Microsoft.AspNetCore.Mvc;

namespace ApiSongs.Controllers
{
        [Route("[controller]/[action]")]
        public class ArtistController : Controller
        {
        private IArtistData _artistData;

        public ArtistController(IArtistData artistData)
        {
            _artistData = artistData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var artists = _artistData.GetAll();
            return new ObjectResult(artists);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var artist = _artistData.Get(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateArtistView createArtistView)
        {
            var newArtist = new ArtistModel
            {
                Name = createArtistView.Name,
                NumberOfSongs = createArtistView.NumberOfSongs,
                MonthlyStreams = createArtistView.MonthlyStreams,
                Popularity = createArtistView.Popularity
            };

            _artistData.Add(newArtist);
            return CreatedAtAction(nameof(Detail), new { newArtist.Id }, newArtist);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var artist = _artistData.Get(id);

            if (artist == null)
            {
                return NotFound();
            }
            _artistData.Delete(artist);

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateArtistView updateArtistView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var artist = _artistData.Get(id);

            if (artist == null)
            {
                return NotFound();
            }
            var updatedArtist = new ArtistModel
            {
                Id = artist.Id,
                Name = updateArtistView.Name,
                NumberOfSongs = updateArtistView.NumberOfSongs,
                MonthlyStreams = updateArtistView.MonthlyStreams,
                Popularity = updateArtistView.Popularity
                
            };

            _artistData.Update(updatedArtist);
            return NoContent();
        }
    }
    }
