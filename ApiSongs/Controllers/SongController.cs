using ApiSongs.Models;
using ApiSongs.Services;
using ApiSongs.View;
using Microsoft.AspNetCore.Mvc;

namespace ApiSongs.Controllers
{
    [Route("[Controller]/[action]")]
    public class SongController : Controller
    {
        private ISongData _songData;

        public SongController(ISongData songData)
        {
            _songData = songData;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var songs = _songData.GetAll();
            return new ObjectResult(songs);
        }

        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var song = _songData.Get(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateSongView createSongView)
        {
            var newSong = new SongModel
            {
                Name = createSongView.Name,
                Artist = createSongView.Artist,
                Streams = createSongView.Streams
            };

            _songData.Add(newSong);
            return CreatedAtAction(nameof(Detail), new { newSong.Id }, newSong);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _songData.Get(id);

            if (song == null)
            {
                return NotFound();
            }
            _songData.Delete(song);

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSongView updateSongView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var song = _songData.Get(id);

            if (song == null)
            {
                return NotFound();
            }
            var updatedSong = new SongModel
            {
                Id = song.Id,
                Name = updateSongView.Name,
                Artist = updateSongView.Artist,
                Streams = updateSongView.Streams
            };

            _songData.Update(updatedSong);
            return NoContent();
        }
    }
}
