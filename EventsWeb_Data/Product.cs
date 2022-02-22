using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Data
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Organisedby { get; set; }
        [Required]
        public DateTime StartsAt { get; set; }
        [Required]
        public DateTime EndsAt { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }

        public int CategoryId { get; set; } = 1;
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
