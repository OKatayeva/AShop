using System;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace AShop_Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public MailjetSettings _MailjetSettings { get; set; }

        public EmailSender (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string messageBody)
        {
            _MailjetSettings = _configuration.GetSection("Mailjet").Get<MailjetSettings>();

            
            MailjetClient client = new MailjetClient(_MailjetSettings.ApiKey, _MailjetSettings.SecretKey)
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "oksana89@gmail.com"},
        {"Name", "AShop"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
          email
         }, {
          "Name",
          "AShop"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
       messageBody
      }
     }
             });
            await client.PostAsync(request);
        }
    }
}
