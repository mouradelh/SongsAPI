using Microsoft.EntityFrameworkCore;
namespace ApiSongs.Models
{
    public class SongsApiDbContext : DbContext
    {
        public SongsApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PlatformModel> Platforms { get; set; }
        public DbSet<ArtistModel> Artists { get; set; }
        public DbSet<SongModel> Songs { get; set; }
    }
}
