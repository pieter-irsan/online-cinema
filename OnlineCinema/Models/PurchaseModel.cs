using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineCinema.Models
{
    public class PurchaseModel
    {
        [Display(Name = "Purchase ID")]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "Rp {0}")]
        public int Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy}")]
        public DateTime Time { get; set; }
    }
}