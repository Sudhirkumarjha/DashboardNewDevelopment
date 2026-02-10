using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace VideoAssetManager.DataAccess.Common
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public MailRecipient From { get; set; }

        public IEnumerable<MailRecipient> CopyTo { get; set; }

        public IEnumerable<MailRecipient> BlindCopyTo { get; set; }

        public string Message { get; set; }

        public IEnumerable<MailRecipient> To { get; set; }

        public MailRecipient ReplyTo { get; set; }     

        public bool HasAttachment { get; set; }

        public List<string> AttachmentPaths { get; set; }

        [JsonIgnore]
        public bool HasProcessed { get; set; } = false;
    }
   
}
