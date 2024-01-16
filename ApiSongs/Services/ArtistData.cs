using ApiSongs.Models;

namespace ApiSongs.Services
{
    public interface IArtistData
    {
        IEnumerable<ArtistModel> GetAll();
        ArtistModel Get(int Id);
        void Add(ArtistModel artist);
        void Delete(ArtistModel artist);
        void Update(ArtistModel artist);
    }
    public class ArtistData : IArtistData
    {
        private static List<ArtistModel> _artists;
        static ArtistData()
        {
            _artists = new List<ArtistModel>
            {
                new ArtistModel { Id = 1, Name = "De Fellas", MonthlyStreams = 1450235, NumberOfSongs = 15, Popularity = 4},
                new ArtistModel { Id = 2, Name = "Broederliefde", MonthlyStreams = 520124, NumberOfSongs = 75, Popularity = 3},
                new ArtistModel { Id = 3, Name = "Frsh", MonthlyStreams = 2846321, NumberOfSongs = 65, Popularity = 5},
                new ArtistModel { Id = 4, Name = "Zack Fox", MonthlyStreams = 3897125, NumberOfSongs = 4, Popularity = 6},
                new ArtistModel { Id = 5, Name = "Inna", MonthlyStreams = 3251478, NumberOfSongs = 11, Popularity = 6},
                new ArtistModel { Id = 6, Name = "Skepta", MonthlyStreams = 10254235, NumberOfSongs = 25, Popularity = 8},
                new ArtistModel { Id = 7, Name = "Lady Gaga", MonthlyStreams = 12458684, NumberOfSongs = 45, Popularity = 9}
            };
        }
        public IEnumerable<ArtistModel> GetAll()
        {
            return _artists;
        }
        public ArtistModel Get(int id)
        {
            return _artists.FirstOrDefault(x => x.Id == id);
        }
        public void Add(ArtistModel artist)
        {
            artist.Id = _artists.Max(x => x.Id) + 1;
            _artists.Add(artist);
        }
        public void Delete(ArtistModel artist)
        {
            _artists.Remove(artist);
        }
        public void Update(ArtistModel artist)
        {
            var oldArtist = Get(artist.Id);
            oldArtist.Name = artist.Name;
            oldArtist.NumberOfSongs = artist.NumberOfSongs;
            oldArtist.Popularity = artist.Popularity;
        }
    }
    public class SqlArtistData : IArtistData
    {
        private SongsApiDbContext _context;

        public SqlArtistData(SongsApiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ArtistModel> GetAll()
        {
            return _context.Artists;
        }
        public ArtistModel Get(int id)
        {
            return _context.Artists.FirstOrDefault(x => x.Id == id);
        }
        public void Add(ArtistModel newArtist)
        {
            _context.Artists.Add(newArtist);
            _context.SaveChanges();
        }
        public void Delete(ArtistModel artist)
        {
            var toDelete = Get(artist.Id);
            _context.Artists.Remove(toDelete);
            _context.SaveChanges();
        }
        public void Update(ArtistModel artist)
        {
            var toUpdate = Get(artist.Id);
            toUpdate.Name = artist.Name;
        }
    }
}
