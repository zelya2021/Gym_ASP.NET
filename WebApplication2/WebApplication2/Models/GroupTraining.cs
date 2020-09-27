using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class GroupTraining
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public DateTime Day { get; set; }
        public Trainer Trainer { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}