using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWeb.Shared.Model
{
    public class EventsCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Enter The Size of the Venue")]
        [Range(1,int.MaxValue,ErrorMessage ="Please Enter Expected Number of People in a event")]
        public int Size { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }    
    }
}
