using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MAWS.Services.EmailNotification
{
    public class Message
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string To { get; set; } //List<string>
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string From { get; set; }
        
        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }

        public Message()
        {
        }

        public string ListToString(List<string> list){
            return string.Join(',', list);
        }

    }
}
