using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace BlazorSSR
{
    public class CaptchaValidationAttribute : ValidationAttribute
    {
        public IFormCollection form { get; set; }
        public override bool IsValid(object value)
        {
            
            
                foreach (var kvp in form)
                {

                }
           
            string recaptcha = value as string;         
            var secretKey = "6LfKO4krAAAAAOZMHEmBIUe1bCTal26ErQ9sO8Bd";
            var apiUrl = "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}";
            var requestUri = string.Format(apiUrl, secretKey,recaptcha);
            var http = new HttpClient();
            var syncTask = Task.Run(() => http.GetStringAsync(requestUri));            
            JObject jResponse = JObject.Parse(syncTask.Result);
            var isSuccess = jResponse.Value<bool>("success");
            return isSuccess;
        }
    }
    
}
