using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class IndividualTraining
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public DateTime Day { get; set; }
        public Trainer Trainer { get; set; }
        public Client Client { get; set; }
    }
}