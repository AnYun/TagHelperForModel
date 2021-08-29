using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TagHelperForModel.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Display(Name = "帳號", Description = "請不要輸入 Admin 作為帳號！")]
        [Required(ErrorMessage = "請輸入 {0}")]
        public string Account { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入 {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
