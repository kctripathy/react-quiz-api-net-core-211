﻿using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
