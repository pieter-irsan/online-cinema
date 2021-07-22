using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OnlineCinema.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }

        [Required(ErrorMessage = "Synopsis is required")]
        [DataType(DataType.MultilineText)]
        public string Synopsis { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DisplayFormat(DataFormatString = "Rp {0}")]
        [Range(5000, 50000, ErrorMessage = "Price range is Rp 5000 to Rp 50000")]
        public int Price { get; set; }

        public string PosterPath { get; set; }

        public HttpPostedFileBase Poster { get; set; }

        [Required(ErrorMessage = "Trailer is required")]
        public string Trailer { get; set; }
    }
}