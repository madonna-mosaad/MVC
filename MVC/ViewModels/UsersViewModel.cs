using System;
using System.Collections.Generic;

namespace MVC.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string lName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public UsersViewModel() 
        {
            Id =Guid.NewGuid().ToString();
        }

    }
}
