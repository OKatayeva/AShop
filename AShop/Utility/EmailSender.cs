using System;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace AShop.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public async Task Execute(string email, string subject, string messageBody)
        {
            MailjetClient client = new MailjetClient("536f693f90b7e2e65382e265ea4262ce", "32ea04c2f8e9eb2b07ab422c01aa7d43")
            {
                //Version = ApiVersion.V3_1,
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
        {"Name", "Oksana"}
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
