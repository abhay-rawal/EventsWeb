using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events_Data;

namespace EventsWeb.Shared.Model
{
    public class EventsProduct
    {
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
     
        public Category? Category { get; set; }
    }
}
