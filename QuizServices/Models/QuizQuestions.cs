﻿using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizQuestions
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public long? CorrectAnswerOptionId { get; set; }
        public short? QuestionTypeId { get; set; }
        public int? ClassSubjectId { get; set; }
        public int? QuizMasterId { get; set; }
    }
}