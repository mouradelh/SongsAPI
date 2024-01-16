namespace ApiSongs.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistModel Artist { get; set; }
        public int Streams { get; set; }
    }
}
