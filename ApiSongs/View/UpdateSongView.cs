using ApiSongs.Models;

namespace ApiSongs.View
{
    public class UpdateSongView
    {
        public string Name { get; set; }
        public ArtistModel Artist { get; set; }
        public int Streams { get; set; }
    }
}
