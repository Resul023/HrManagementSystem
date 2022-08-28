using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrManagerMVC.Models
{
    public class SendMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public DateTime createdAt { get; set; }
        public AppUser FromUser { get; set; }
        public AppUser ToUser { get; set; }
    }
}
