using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public string Login { get; set; }
        public ICollection<GroupTraining> GroupTrainings { get; set; }
        public ICollection<IndividualTraining> IndividualTrainings { get; set; }
        public Trainer Trainer { get; set; }
    }
}