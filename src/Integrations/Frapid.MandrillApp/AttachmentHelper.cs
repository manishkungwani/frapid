using System;
using System.Collections.Generic;
using System.IO;
using Mandrill.Models;

namespace Frapid.MandrillApp
{
    internal static class AttachmentHelper
    {
        internal static EmailMessage AddAttachments(EmailMessage message, string[] attachments)
        {
            if (attachments != null)
            {
                var emailAttachments = new List<EmailAttachment>();
                foreach (string file in attachments)
                {
                    if (!String.IsNullOrWhiteSpace(file))
                    {
                        if (File.Exists(file))
                        {
                            var content = File.ReadAllBytes(file);
                            emailAttachments.Add(new EmailAttachment() { Content = Convert.ToBase64String(content), Name = Path.GetFileName(file) });
                        }
                    }
                }
            }
            return message;
        }
    }
}