using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestarauntWebApplication.Models
{
    public class LoginModel
    {
        [Required (ErrorMessage = "Требуется логин")]
        public string Login { get; set; }
        //[Range(1, 40, ErrorMessage = "Пароль не больше 40 символов")]
        [Required(ErrorMessage = "Требуется пароль")]
        public string Password { get; set; }



    }
}
