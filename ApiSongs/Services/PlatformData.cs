using ApiSongs.Models;

namespace ApiSongs.Services
{
    public interface IPlatfromData
    {
        IEnumerable<PlatformModel> GetAll();
        PlatformModel Get(int Id);
        void Add(PlatformModel platform);
        void Delete(PlatformModel platform);
        void Update(PlatformModel platform);
    }
    public class PlatformData : IPlatfromData
    {
        private static List<PlatformModel> _platforms;
        static PlatformData()
        {
            _platforms = new List<PlatformModel>
            {
                new PlatformModel { Id = 1, Name = "Spotify"},
                new PlatformModel { Id = 2, Name = "Apple Music"},
                new PlatformModel { Id = 3, Name = "Youtube"}
            };
        }


        public IEnumerable<PlatformModel> GetAll()
        {
            return _platforms;
        }
        public PlatformModel Get(int id)
        {
            return _platforms.FirstOrDefault(x => x.Id == id);
        }
        public void Add(PlatformModel platform)
        {
            platform.Id = _platforms.Max(x => x.Id) + 1;
            _platforms.Add(platform);
        }
        public void Delete(PlatformModel platform)
        {
            _platforms.Remove(platform);
        }
        public void Update(PlatformModel platform)
        {
            var oldPlatform = Get(platform.Id);
            oldPlatform.Name = platform.Name;
        }

        
    }
    public class SqlPlatformData : IPlatfromData
    {
        private SongsApiDbContext _context;

        public SqlPlatformData(SongsApiDbContext context)
        {
            _context = context;
        }
        public IEnumerable<PlatformModel> GetAll()
        {
            return _context.Platforms;
        }
        public PlatformModel Get(int id)
        {
            return _context.Platforms.FirstOrDefault(x => x.Id == id);
        }
        public void Add(PlatformModel newPlatform)
        {
            _context.Platforms.Add(newPlatform);
            _context.SaveChanges();
        }
        public void Delete(PlatformModel platform)
        {
            var toDelete = Get(platform.Id);
            _context.Platforms.Remove(toDelete);
            _context.SaveChanges();
        }
        public void Update(PlatformModel platform)
        {
            var toUpdate = Get(platform.Id);
            toUpdate.Name = platform.Name;
        }
    }
}
