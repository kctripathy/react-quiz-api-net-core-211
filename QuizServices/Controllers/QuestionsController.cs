using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace QuizServices.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuestionsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public IActionResult Get()
        {
            return Get(17,2);
            //return _context.QuizQuestions.ToList();
            //return new string[] { "Questions value1", "Questions value2" };
        }     

        [HttpGet("{classSubjectid}/{quizId}", Name = "GetQuestionsByClassSubjectAndQuizId")]
        public IActionResult Get(int classSubjectid, int quizId)
        {
            try
            {
                QuizQuestionsViewModel qvm =  GetQuizQuestionMasterOutput(classSubjectid, quizId);

                if (qvm == null)                    
                    return BadRequest(string.Format(@"[No Questions for classSubjectid={0} and quizId={1} ]", classSubjectid, quizId));
                else
                    return Ok(qvm);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }



        private QuizQuestionsViewModel GetQuizQuestionMasterOutput(int classSubjectId, int quizId)
        {
            var QuizMaster = _context
                                    .QuizMaster
                                    .FromSql($"GetQuizMasterByID {quizId}")
                                    .ToList();

            if (QuizMaster.Count.Equals(0)) return null;

            QuizQuestionsViewModel qvm = new QuizQuestionsViewModel
            {
                id = QuizMaster[0].Id,
                name = QuizMaster[0].Name,
                description = QuizMaster[0].Description,
                questions = GetAllQuestions(classSubjectId, quizId)
            };
            return qvm;
        }


        private List<Question> GetAllQuestions(int classSubjectId, int quizId)
        {

            var QuestionList = _context
                                    .QuizQuestions
                                    .FromSql($"GetQuestions {classSubjectId}, {quizId}")
                                    .ToList();

            var OptionList = _context
                                    .QuizQuestionsOptions
                                    .FromSql($"GetQuestions {classSubjectId}, {quizId},'Options'")
                                    .ToList();


            List<Question> questionlist = new List<Question>();
            for (int ctr = 1; ctr <= QuestionList.Count; ctr++)
            {
                Question q = new Question
                {
                    id = QuestionList[ctr - 1].Id,
                    name = QuestionList[ctr - 1].Description, //string.Format("Q#{0} - {1}", ctr, QuestionList[ctr - 1].Description),
                    options = GetAllOptions(QuestionList[ctr - 1].Id, OptionList, QuestionList[ctr - 1].CorrectAnswerOptionId)
                };
                q.questionType = GetQuestionTypes(q.questionTypeId);
                questionlist.Add(q);
            }


            return questionlist;
        }


        private List<Option> GetAllOptions(int questionId, List<QuizQuestionsOptions> optionList, long? correctAnswerOptionId)
        {

            List<QuizQuestionsOptions> questionOptions = optionList.FindAll(a => a.QuestionId == questionId);

            // Adding options for the question
            List<Option> oList = new List<Option>();
            int ctr = 0;
            foreach (QuizQuestionsOptions opt in questionOptions)
            {
                ctr++;
                Option o = new Option();
                o.id = opt.Id;
                o.questionId = questionId;
                o.name = opt.Description; //string.Format("Option {0}-{1} | {2} ", questionId, ctr, opt.Description);
                o.isAnswer = (opt.Id == correctAnswerOptionId);
                oList.Add(o);
            }

            return oList;
        }



        private QuestionType GetQuestionTypes(int questionTypeId)
        {
            QuestionType qt = new QuestionType();
            qt.id = 1;
            qt.name = "Multiple Choice";
            qt.isActive = true;
            return qt;
        }
    }
}
