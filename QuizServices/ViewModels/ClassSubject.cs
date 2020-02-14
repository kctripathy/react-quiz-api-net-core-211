using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class ClassSubject
    {
        public int ClassSubjectID { get; set; }

        public int ClassID { get; set; }
        public string ClassDesc { get; set; }

        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDesc { get; set; }
    }
}
