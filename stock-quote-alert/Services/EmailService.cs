using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stock_quote_alert.Dto;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;

namespace stock_quote_alert.Services
{
    public class EmailService
    {
        public bool sendEmail(string body, string subject, bool isBodyHtml = false, string? name = null)
        {
            if (body != null && subject != null)
            {
                dynamic? configs = null;

                // Read the configs in configs json
                StreamReader r = new("../../../Configs/emailSettings.json");
                if(r != null)
                {
                    string? json = null;
                    using (r)
                    {
                        json = r.ReadToEnd();
                        if(json != null)
                            configs = JObject.Parse(json);
                    }

                    if(configs != null)
                    {
                        try
                        {
                            SmtpClient Client = new()
                            {
                                Host = configs.Host.ToString(),
                                Port = configs.Port,
                                EnableSsl = configs.EnableSSL,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = configs.UseDefaultCredentials,
                                Credentials = new NetworkCredential()
                                {
                                    UserName = configs.Username.ToString(),
                                    Password = configs.Password.ToString()
                                }
                            };

                            MailAddress FromEmail = new(configs.Username.ToString(),"Monitoramento de Preço");
                            MailAddress ToEmail = new(configs.EmailTo.ToString());
                            MailMessage Message = new()
                            {
                                From = FromEmail,
                                Subject = subject,
                                Body = body,
                                IsBodyHtml = isBodyHtml
                            };
                            Message.To.Add(ToEmail);


                            Client.Send(Message);
                        }
                        catch (System.Exception ex)
                        {
                            Exception? innerException = ex.InnerException;
                            if (innerException != null)
                            {
                                ex = innerException;
                            }

                            throw ex;
                        }
                        
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

