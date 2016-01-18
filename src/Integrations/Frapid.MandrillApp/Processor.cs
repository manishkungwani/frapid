using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Frapid.Messaging;
using Frapid.Messaging.DTO;
using Mandrill;
using Mandrill.Requests.Messages;
using Serilog;

namespace Frapid.MandrillApp
{
    public class Processor : IEmailProcessor
    {
        public IEmailConfig Config { get; set; }
        public bool IsEnabled { get; set; }

        public void InitializeConfig(string catalog)
        {
            var config = MandrillApp.Config.Get(catalog);
            this.Config = config;

            this.IsEnabled = this.Config.Enabled;

            if (!this.IsEnabled)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                this.IsEnabled = false;
            }
        }

        public async Task<bool> SendAsync(EmailMessage email)
        {
            return await this.SendAsync(email, false, null);
        }

        public async Task<bool> SendAsync(EmailMessage email, bool deleteAttachmentes, params string[] attachments)
        {
            var config = this.Config as Config;

            if (config == null)
            {
                email.Status = Status.Cancelled;
                return false;
            }

            try
            {
                email.Status = Status.Executing;


                var message = new Mandrill.Models.EmailMessage();
                message.RawTo = email.SentTo.Split(',').ToList();
                message.FromEmail = email.FromEmail;
                message.FromName = email.FromName;
                message.Subject = email.Subject;
                if (email.IsBodyHtml)
                {
                    message.Html = email.Message;
                }
                else
                {
                    message.Text = email.Message;
                }
                message = AttachmentHelper.AddAttachments(message, attachments);
                var sendMessageRequest = new SendMessageRequest(message);

                var api = new MandrillApi(config.ApiKey);
                var result = await api.SendMessage(sendMessageRequest);

                var status = result.First().Status;
                // Verify
                if (status == Mandrill.Models.EmailResultStatus.Invalid || status == Mandrill.Models.EmailResultStatus.Rejected)
                {
                    email.Status = Status.Failed;
                }
                else if (status == Mandrill.Models.EmailResultStatus.Queued || status == Mandrill.Models.EmailResultStatus.Scheduled)
                {
                    email.Status = Status.Executing;
                }
                else if (status == Mandrill.Models.EmailResultStatus.Sent)
                {
                    email.Status = Status.Completed;
                }
                return true;
            }
            // ReSharper disable once CatchAllClause
            catch (Exception ex)
            {
                email.Status = Status.Failed;
                Log.Warning(@"Could not send email to {To} using SendGrid API. {Ex}. ", email.SentTo, ex);
            }
            finally
            {
                if (deleteAttachmentes)
                {
                    FileHelper.DeleteFiles(attachments);
                }
            }

            return false;
        }
    }
}