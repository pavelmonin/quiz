using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace quiz_backend.Controllers
{
    [Produces("application/json")]
    [Route("api/Questions")]
    public class QuestionsController : Controller
    {

        readonly QuizContext context;

        public QuestionsController(QuizContext context)
        {
            this.context = context;
        }



        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Models.Question question)
        {
            //context.Questions.Add(new Models.Question() {Text = "text1" });

            var quiz = context.Quiz.SingleOrDefault(q => q.Id == question.QuizId);

            if (quiz is null)
            {
                return NotFound();
            }

            context.Questions.Add(question);
            await context.SaveChangesAsync();
            return Ok(question);

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Models.Question> Get()
        {
            //return new Models.Question[] {
            //    new Models.Question() {Text="test21"},
            //    new Models.Question() {Text="test21"},
            //    new Models.Question() {Text="test21"},
            //    new Models.Question() {Text="test21"},         
            //};

            return context.Questions;
        }

        // GET api/values
        [HttpGet("{quizId}")]
        public IEnumerable<Models.Question> Get([FromRoute] int quizId)
        {
            return context.Questions.Where(q => q.QuizId==quizId);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Models.Question question)
        {
            //before
            //var question = await context.Questions.SingleOrDefaultAsync(q => q.Id == id);

            //return Ok(question);

            //comparing id inside body request and id in url
            if (id != question.Id)
                return BadRequest();

            context.Entry(question).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(question);
        }

    }
}