using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male=1,
        [EnumMember(Value ="Female")]
        Female=2
    }

    public class Employee : ModelBase
    {
        //same coments as Department
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(50,ErrorMessage ="maximum length is 50")]
        [MinLength(4, ErrorMessage = "minimum length is 50")]
        public string Name { get; set; }
        [Range(21, 60 ,ErrorMessage ="the range is 21 to 60")]
        public int? Age { get; set; }
        [RegularExpression(@"^\d{1,3}-[a-zA-Z]{4,}-[a-zA-Z]{4,}-[a-zA-Z]{4,}$",
            ErrorMessage ="Address must be like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive {  get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name ="Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        //soft Delete ( lw eldata msh 3ayzaha f elwebsite bs 3ayzaha f elDB yb2a mms7hash la a3ml el IsDeleted b true)
        public bool IsDeleted { get; set; }
        public Gender Gender { get; set; }
    }
}
