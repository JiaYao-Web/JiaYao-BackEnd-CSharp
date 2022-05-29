using System;
using System.Collections.Generic;

#nullable disable

namespace JiaYao.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string Introduction { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
