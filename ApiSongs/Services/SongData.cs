using ApiSongs.Models;

namespace ApiSongs.Services
{
    public interface ISongData
    {
        IEnumerable<SongModel> GetAll();
        SongModel Get(int Id);
        void Add(SongModel song);
        void Delete(SongModel song);
        void Update(SongModel song);

    }
    public class SongData : ISongData
    {
        private static List<SongModel> _songs;
        private readonly IArtistData _artistdata;
        public SongData(IArtistData artistData) 
        {
            _artistdata = artistData;
            _songs = new List<SongModel>
            {
                new SongModel { Id = 1, Artist = artistData.Get(1), Name = "Interlude", Streams = 452300 },
                new SongModel { Id = 2, Artist = artistData.Get(1), Name = "Deja Vu", Streams = 2211300 },
                new SongModel { Id = 3, Artist = artistData.Get(1), Name = "Appel Mint Puur", Streams = 785123 },
                new SongModel { Id = 4, Artist = artistData.Get(1), Name = "IHY2LN", Streams = 9512123 },
                new SongModel { Id = 5, Artist = artistData.Get(1), Name = "Niemand", Streams = 741200 },
                new SongModel { Id = 6, Artist = artistData.Get(1), Name = "Scheuren", Streams = 350122 },
                new SongModel { Id = 7, Artist = artistData.Get(1), Name = "Mazzeltov", Streams = 854210 },
                new SongModel { Id = 8, Artist = artistData.Get(1), Name = "Streetvibes", Streams = 1256001 },

            };
        }
        public IEnumerable<SongModel> GetAll()
        {
            return _songs;
        }
        public SongModel Get(int id)
        {
            return _songs.FirstOrDefault(x => x.Id == id);
        }
        public void Add(SongModel song)
        {
            song.Id = _songs.Max(x => x.Id) + 1;
            _songs.Add(song);
        }
        public void Delete(SongModel song)
        {
            _songs.Remove(song);
        }
        public void Update(SongModel song)
        {
            var oldSong = Get(song.Id);
            oldSong.Name = song.Name;
            oldSong.Streams = song.Streams;
        }
    }
    public class SqlSongData : ISongData
    {
        private SongsApiDbContext _context;

        public SqlSongData(SongsApiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SongModel> GetAll()
        {
            return _context.Songs;
        }
        public SongModel Get(int id)
        {
            return _context.Songs.FirstOrDefault(x => x.Id == id);
        }
        public void Add(SongModel newSong)
        {
            _context.Songs.Add(newSong);
            _context.SaveChanges();
        }
        public void Delete(SongModel song)
        {
            var toDelete = Get(song.Id);
            _context.Songs.Remove(toDelete);
            _context.SaveChanges();
        }
        public void Update(SongModel song)
        {
            var toUpdate = Get(song.Id);
            toUpdate.Name = song.Name;
        }
    }
}
