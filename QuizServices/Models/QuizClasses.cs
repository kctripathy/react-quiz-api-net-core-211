using QuizServices.Data;
using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizClasses: IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
