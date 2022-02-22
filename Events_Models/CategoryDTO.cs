using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Enter The Size of the Venue")]
        [Range(1,int.MaxValue,ErrorMessage ="Please Enter Expected Number of People in a event")]
        public string Size { get; set; }
        public string Description { get; set; }
    }
}
