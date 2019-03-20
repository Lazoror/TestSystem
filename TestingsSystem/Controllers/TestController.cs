using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestingsSystem.DAL;
using TestingsSystem.Models;

namespace TestingsSystem.Controllers
{
    public class TestController : Controller
    {
        TestContext db = new TestContext();

        [HttpGet]
        public ActionResult Index(int? id)
        {
            Test test = db.Tests.ToList().Find(a => a.TestID == id);
            List<Question> questions = db.Questions.ToList().FindAll(a => a.TestID == id);
            var answers = db.Answers.ToList().FindAll(a => a.TestID == id);

            AnswerView av = new AnswerView();
            av.Answers = new List<Answer>();

            foreach (Answer item in answers)
            {
                item.IsTrueAnswer = false;
                av.Answers.Add(item);
            }


            ViewBag.Questions = questions;
            ViewBag.Test = test;

            return View(av);

        }



        [HttpPost]
        public ActionResult Index(AnswerView av)
        {
            int mistakes = 0;
            int rightAnswers = 0;
            int rightAnswersCount = db.Answers.ToList().Where(a => a.TestID == av.Answers[0].TestID && a.IsTrueAnswer == true).Count();
            

            foreach (Answer item in av.Answers)
            {
                var curAnswer = db.Answers.Find(item.AnswerID).IsTrueAnswer;

                if ((curAnswer == true) && (item.IsTrueAnswer == true))
                {
                    rightAnswers++;
                }
                if ((curAnswer == false) && (item.IsTrueAnswer == true))
                {
                    mistakes++;
                }
            }


            return RedirectToAction("Result", new {rightAnswers, rightAnswersCount, mistakes });
        }

        public ActionResult Result(int rightAnswers, int rightAnswersCount, int mistakes)
        {
            ViewBag.RightAnswers = rightAnswers;
            ViewBag.AnswerCount = rightAnswersCount;
            ViewBag.Mistakes = mistakes;

            return View();
        }

        [HttpPost]
        // POST: Test
        public ActionResult Create([Bind(Include = "TestName, TestObject, QuestionCount")] Test t)
        {
            if (ModelState.IsValid)
            {
                db.Tests.Add(t);
                db.SaveChanges();

                return RedirectToAction("CreateQuestion", new { @id = t.QuestionCount, @Tid = t.TestID });
            }

            return View(t);
            
        }
        
        
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateQuestion([Bind(Include = "QuestionName, AnswerCount, TestID")] Question q, int id, int Tid)
        {

            if (q != null)
            {
                q.TestID = Tid;

                for (int i = 0; i < id; i++)
                {
                    db.Questions.Add(q);
                    db.SaveChanges();

                }
            }

            Question qt = new Question();
            qt = db.Questions.First(a => a.TestID == q.TestID);
            

            return RedirectToAction("Question", new { @id = q.TestID, @Question = qt.QuestionID });
        }


        [HttpPost]
        public ActionResult Question(Question q)
        {
            Question qTemp = new Question();
            qTemp = db.Questions.Find(q.QuestionID);
            qTemp.QuestionID = q.QuestionID;
            qTemp.TestID = q.TestID;
            qTemp.QuestionName = q.QuestionName;
            qTemp.AnswerCount = q.AnswerCount;
            db.Entry(qTemp).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("CreateAnswer", new { @Answer = q.AnswerCount, @Question = q.QuestionID, @id = q.TestID });
        }

        [HttpGet]
        public ActionResult Question(int? id, int? Question)
        {

            if(db.Questions.Find(Question) == null || db.Tests.Find(id) == null)
            {
                return RedirectToAction("Error");
            }

            var qNext = db.Questions.Where(a => a.QuestionID == Question).First();
            var qFirst = db.Questions.Where(a => a.TestID == id).First();
            var TestEntity = db.Tests.Where(a => a.TestID == qNext.TestID).First();

            ViewBag.QuestionNum = (Question - qFirst.QuestionID) + 1;
            ViewBag.TestId = id;
            ViewBag.TestName = TestEntity.TestName;
            ViewBag.TestNum = TestEntity.QuestionCount;

            return View(qNext);

        }

        
        public ActionResult CreateAnswer([Bind(Include = "AnswerName, IsTrueAnswer, QuestionID")] Answer a, int Answer, int id, int Question)
        {
            a.QuestionID = Question;

            for (int i = 0; i < Answer; i++)
            {
                db.Answers.Add(a);
                db.SaveChanges();
            }

            return RedirectToAction("Answer", new { @Question = Question, @id = id });
        }

        [HttpPost]
        public ActionResult Answer(AnswerView answer, int Question, int id)
        {
            int qCheck = ++Question;

            for (int i = 0; i < answer.Answers.Count; i++)
            {
                var curr = answer.Answers[i];

                Answer answerTemp = db.Answers.Find(curr.AnswerID);
                answerTemp.AnswerName = curr.AnswerName;
                answerTemp.IsTrueAnswer = curr.IsTrueAnswer;
                answerTemp.TestID = id;
                db.Entry(answerTemp).State = EntityState.Modified;
                db.SaveChanges();

            }

            if (db.Questions.Find(qCheck) != null)
            {
                return RedirectToAction("Question", new { @id = id, @Question = qCheck });
            }

            return RedirectToAction("Index", new { @id = id});
        }


        [HttpGet]
        public ActionResult Answer(int Question, int? id)
        {
            AnswerView v = new AnswerView();
            v.Answers = new List<Answer>();

            var answers = db.Answers.ToList().FindAll(a => a.QuestionID == Question);

            if (db.Questions.Find(Question) == null)
            {
                return RedirectToAction("Error");
            }


            foreach (var item in answers)
            {
                v.Answers.Add(item);
            }

            var qFirst = db.Questions.Where(a => a.TestID == id).First();

            ViewBag.QuestionNum = (Question - qFirst.QuestionID) + 1;
            ViewBag.TestId = id;
            ViewBag.QuestionId = Question;

            return View(v);
        }


        public ActionResult Error()
        {
            return View();
        }

    }
}