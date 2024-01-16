using System.ComponentModel.DataAnnotations;

namespace ApiSongs.View
{
    public class CreateArtistView
    {
        private double _popularity;
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public int NumberOfSongs { get; set; }
        public int MonthlyStreams { get; set; }
        public double Popularity
        {
            get { return _popularity; }
            set
            {
                if (value > 0 && value <= 10)
                {
                    _popularity = value;
                }
                else
                {
                    throw new ArgumentException("Popularity must be between 0 and 10.");
                }
            }
        }
    }
}
