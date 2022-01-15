using HandlebarsDotNet;
using Quickstart.Core.BL.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Quickstart.Core.BL.Services
{
    public class EmailService
    {
        /// <summary>
        /// Metodo encargado de recuperar la plantilla html e interpolar la informacion
        /// </summary>
        /// <param name="basePathTemplate"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetHtml(string basePathTemplate,
            object data)
        {
            if (!File.Exists(basePathTemplate))
                throw new Exception(string.Format("La plantilla {0} no se ha encontrado en el folder de plantillas", basePathTemplate));

            var templateText = File.ReadAllText(basePathTemplate);
            var template = Handlebars.Compile(templateText);

            return template(data);
        }

        /// <summary>
        /// Metodo encargado de recuperar la notificacion y enviarla via mail
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="basePathTemplate"></param>
        /// <param name="data"></param>
        public void SendNotification(string addresses,
            string subject,
            string pathTemplate,
            dynamic data)
        {
            try
            {
                pathTemplate = $@"{Helpers.Endpoint.UrlAssetsBase}\Assets\Templates\{pathTemplate}";
                var content = GetHtml(pathTemplate, data);

                var linkedResources = new List<LinkedResourceDTO>();
                var attachments = new List<AttachmentDTO>();

                SendNotification(addresses, subject, content, linkedResources, attachments);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        /// <summary>
        /// Metodo encargado de construir el MailMessage
        /// </summary>
        /// <param name="destination">Destino del mail</param>
        /// <param name="subject">Asunto del mail</param>
        /// <param name="content">Contenido html del mail</param>
        /// <param name="linkedResources">Recursos incrustados</param>
        /// <param name="attachments">Adjuntos del mail</param>
        public void SendNotification(string addresses,
            string subject,
            string content,
            List<LinkedResourceDTO> linkedResources,
            List<AttachmentDTO> attachments)
        {
            // Create a message and set up the recipients.
            var mailMessage = new MailMessage();
            mailMessage.To.Add(addresses);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.From = new MailAddress("notificacionesdemodsz@gmail.com", "notificacionesdemodsz");

            var alternativeView = AlternateView.CreateAlternateViewFromString(content, Encoding.UTF8, MediaTypeNames.Text.Html);

            foreach (var item in linkedResources)
            {
                byte[] documentArray = File.ReadAllBytes(item.Path);
                var linkedResource = new LinkedResource(new MemoryStream(documentArray))
                {
                    ContentId = item.ContentId,
                    ContentType = new ContentType(item.ContentType),
                    TransferEncoding = TransferEncoding.Base64
                };
                alternativeView.LinkedResources.Add(linkedResource);
            }

            // Create  the file attachment for this email message.
            foreach (var item in attachments)
            {
                // Add the file attachment to this email message.
                var attachment = new Attachment(item.Path, MediaTypeNames.Application.Octet);
                mailMessage.Attachments.Add(attachment);
            }

            mailMessage.AlternateViews.Add(alternativeView);
            SendEmail(mailMessage);
        }

        /// <summary>
        /// Metodo encargado de enviar el MailMessage a traves del cliente Smtp
        /// </summary>
        /// <param name="mailMessage">MailMessage</param>
        /// <returns></returns>
        public bool SendEmail(MailMessage mailMessage)
        {
            //Send the message.
            var client = new SmtpClient
            {
                //Only test mode
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential("notificacionesdemodsz@gmail.com", "Qwer1234*!"),
                Port = 587,
                EnableSsl = true
            };

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }
    }
}
