using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;

namespace QuizServices.Data.EFCore
{
    public class EfCoreQuestionsRepository: EfCoreRepository<QuizQuestions, QuizContext >
    {
        private readonly QuizContext _context;
        public EfCoreQuestionsRepository(QuizContext context): base(context)
        {
            _context = context;
        }

        #region Get All the Questions Along With Options that will be rendered in the UI (ReactJS)
     
        /// <summary>
        /// This function will return all the questions will be rendered in the front end
        /// </summary>
        /// <param name="classSubjectId">class & subject id</param>
        /// <param name="accountId">account owner for whom the questions will be return</param>
        /// <returns></returns>
        public QuizQuestionsViewModel GetQuizQuestionsByClassSubjctAndAccountId(int classSubjectId, int accountId)
        {
            var QuizClassSubject = _context
                                    .ClassSubject
                                    .FromSql($"GetQuizClassSubjectByID {classSubjectId}")
                                    .ToList();

            if (QuizClassSubject.Count.Equals(0)) return null;

            QuizQuestionsViewModel qvm = new QuizQuestionsViewModel
            {
                id = QuizClassSubject[0].ClassSubjectID,
                name = QuizClassSubject[0].SubjectName,
                description = QuizClassSubject[0].SubjectDesc,
                classname = QuizClassSubject[0].ClassDesc,
                questions = GetAllQuestions(classSubjectId, accountId)
            };
            return qvm;
        }


        private List<Question> GetAllQuestions(int classSubjectId, int accountId)
        {

            var QuestionList = _context
                                    .QuizQuestions
                                    .FromSql($"GetQuestions {classSubjectId}, {accountId}")
                                    .ToList();

            var OptionList = _context
                                    .QuizQuestionsOptions
                                    .FromSql($"GetQuestions {classSubjectId}, {accountId},'Options'")
                                    .ToList();


            List<Question> questionlist = new List<Question>();
            for (int ctr = 1; ctr <= QuestionList.Count; ctr++)
            {
                Question q = new Question
                {
                    Id = QuestionList[ctr - 1].Id,
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

        #endregion

        public int InsertQuestion(Question question)
        {
            int returnValue = 0;
            try
            {
                QuizQuestions ques = new QuizQuestions();
                ques.Description = question.name;
                ques.AccountId = question.accountId;
                ques.ClassSubjectId = question.classSubjectId;

                _context.QuizQuestions.Add(ques);
                _context.SaveChanges();

                int newQuestionId = ques.Id;
                long correctAnswerId = 0;
                foreach (Option o in question.options)
                {
                    QuizQuestionsOptions opt = new QuizQuestionsOptions();
                    opt.Description = o.name;
                    opt.QuestionId = newQuestionId;

                    _context.QuizQuestionsOptions.Add(opt);
                    _context.SaveChanges();

                    if (o.isAnswer)
                    {
                        correctAnswerId = opt.Id;
                    }
                }


                ques.CorrectAnswerOptionId = correctAnswerId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                returnValue = newQuestionId; 
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {
                
            }
            return returnValue;
        }

        public int UpdateQuestion(Question question)
        {
            int returnValue = 0;
            try
            {
                QuizQuestions ques = new QuizQuestions();
                ques.Id = question.Id;
                ques.Description = question.name;
                ques.AccountId = question.accountId;
                ques.ClassSubjectId = question.classSubjectId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                long correctAnswerId = 0;
                foreach (Option o in question.options)
                {
                    QuizQuestionsOptions opt = new QuizQuestionsOptions();
                    opt.Id = o.id;
                    opt.Description = o.name;
                    opt.QuestionId = question.Id;

                    _context.QuizQuestionsOptions.Update(opt);
                    _context.SaveChanges();

                    if (o.isAnswer)
                    {
                        correctAnswerId = opt.Id;
                    }
                }


                ques.CorrectAnswerOptionId = correctAnswerId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                returnValue = ques.Id;
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {

            }
            return returnValue;
        }

        public int DeleteQuestion(int id)
        {
            int returnValue = 0;
            try
            {
                var quizQuestion = _context.QuizQuestions.Find(id);
                if (quizQuestion == null)
                    returnValue = 0;
            }
            catch 
            {
                returnValue = -1;
            }
            return returnValue;
        }
    }
}
