using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is Required")]//servsr-side validation(check after send request to server and response the check result)
        public int Code { get; set; }
        [Required(ErrorMessage = "Name is Required")]//server-side validation(check after send request to server and response the check result)
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")] // the name that appear in website if use the property's name in DisplayNameFor method
        public DateTime DateOfCreation { get; set; }//DateTime is value type so it is required by default(not allow null)
    }
}

