using ApiSongs.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiSongs.View
{
    public class CreateSongView
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public ArtistModel Artist { get; set; }
        public int Streams { get; set; }
    }
}
