using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;



    namespace BackendCom.Models
    {
        public class LoginModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            public string PassHash { get; set; } = null!;
        }
    }

