using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingsSystem.Models
{
    public class Test
    {
        public int TestID { get; set; }
        [Required]
        public string TestName { get; set; }
        public string TestObject { get; set; }
        [Required]
        public int QuestionCount { get; set; }

        public virtual ICollection<Question> Questions { get; set; }


    }
}