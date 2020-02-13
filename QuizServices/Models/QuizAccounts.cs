using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizAccounts
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ContactName { get; set; }
        public string OfficeName { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public byte[] Logo { get; set; }
    }
}
