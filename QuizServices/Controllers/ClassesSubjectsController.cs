using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QuizServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesSubjectsController : ControllerBase
    {


        private readonly QuizContext _context;

        public ClassesSubjectsController(QuizContext context)
        {
            _context = context;
        }


        // GET: api/ClassesSubjects
        [HttpGet]
        public IActionResult Get()
        {
            var availableClassesSubjects = _context
                                            .QuestionAvailaleClassAndSubjects
                                           .FromSql("GetAvailaleClassAndSubjects")
                                           .ToList();

            return Ok(availableClassesSubjects);
        }

        // GET: api/ClassesSubjects/all
        [HttpGet("all", Name = "AllClassesSubjects")]
        public IActionResult GetAll()
        {
            var availableClassesSubjects = _context
                                            .QuestionAvailaleClassAndSubjects
                                           .FromSql($"GetAllClassesSubjects")
                                           .ToList();

            return Ok(availableClassesSubjects);
        }

        // GET: api/ClassesSubjects/1
        [HttpGet("{classId}", Name = "ClassesSubjectsByClassId")]
        public IActionResult Get(int classId)
        {
            var availableClassesSubjects = _context
                                            .QuestionAvailaleClassAndSubjects
                                           .FromSql($"GetAvailaleClassAndSubjects {classId}")
                                           .ToList();

            return Ok(availableClassesSubjects);
        }


        // GET: api/ClassesSubjects/1
        [HttpGet("{classId}/{accountId}", Name = "ClassesSubjectsByClassAndAccountId")]
        public IActionResult Get(int classId, int accountId)
        {
            var availableClassesSubjects = _context
                                            .QuestionAvailaleClassAndSubjects
                                           .FromSql($"GetAvailaleClassAndSubjects {classId}, {accountId}")
                                           .ToList();

            return Ok(availableClassesSubjects);
        }
    }
}
