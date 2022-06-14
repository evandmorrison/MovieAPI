using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Database.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReleaseYear { get; set; }
        public decimal Duration { get; set; }
        public int Rating { get; set; }
    }
}