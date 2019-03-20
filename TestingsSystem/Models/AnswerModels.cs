using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingsSystem.Models
{
    
        public class Answer
        {

            public int AnswerID { get; set; }
            public string AnswerName { get; set; }
            public bool IsTrueAnswer { get; set; }
            public int QuestionID { get; set; }
            public int TestID { get; set; }

            public virtual Question Question { get; set; }
        }

        public class AnswerView
        {
            public List<Answer> Answers { get; set; }
        }

            


    
}