namespace ApiSongs.Models
{
    public class ArtistModel
    {
        private double _popularity;
        public int Id { get; set; }
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
