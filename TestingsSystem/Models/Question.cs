using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingsSystem.Models
{
    public class Question
    {

        public int QuestionID { get; set; }

        
        public string QuestionName { get; set; }
        
        public int AnswerCount { get; set; }
        public int TestID { get; set; }


        public virtual Test Tests { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}