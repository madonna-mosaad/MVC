using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department:ModelBase
    {
        [Required(ErrorMessage = "code is Required")]//servsr-side ( when use ModelState.IsValid) and client-side (when use jquery-Validation) validation
        public int Code { get; set; }
        [Required(ErrorMessage = "Name is Required")]//servsr-side ( when use ModelState.IsValid) and client-side (when use jquery-Validation) validation
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")] // the name that appear in website if use the property's name in DisplayNameFor method
        public DateTime DateOfCreation { get; set; }//DateTime is value type so it is required by default(not allow null)
        public ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();//Navigation property
    }
}

