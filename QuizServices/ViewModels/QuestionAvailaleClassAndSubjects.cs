﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class AvailaleClassAndSubjects
    {
        [Key]
        public int ClassSubjectID { get; set; }

        public int ClassID { get; set; }
        public string ClassDesc { get; set; }

        public int SubjectID { get; set; }
        public string SubjectDesc { get; set; }

        public int AccountId { get; set; }  
        public string AccountName { get; set; }

    }

    public class AllClassesAndSubjects
    {
        [Key]
        public int ClassSubjectID { get; set; }

        public int ClassID { get; set; }
        public string ClassDesc { get; set; }

        public int SubjectID { get; set; }
        public string SubjectDesc { get; set; }

        public int AccountId { get; set; }
        public string AccountName { get; set; }

    }
}
