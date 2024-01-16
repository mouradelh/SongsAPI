using System.ComponentModel.DataAnnotations;

namespace ApiSongs.View
{
    public class UpdatePlatformView
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }
    }
}
