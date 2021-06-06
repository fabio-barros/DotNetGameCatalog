using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.InputModel
{
    public class GameInputModel
    {
        
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title length must at least 2 characters long")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Developer name must at least 2 characters long")]
        public string Developer { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Publisher name must at least 2 characters long")]
        public string Publisher { get; set; }

        [Required]
        //[DataType(DataType.DateTime, ErrorMessage = "Invalid release date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Year { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be at least 1,00")]
        public decimal Price { get; set; }
    }
}
