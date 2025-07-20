using System.ComponentModel.DataAnnotations;

namespace BlazorSSR
{
    public class Contact
    {     
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The captcha is not valid!")]
        public bool IsRecaptchaValid { get; set; }
        
    }
}
