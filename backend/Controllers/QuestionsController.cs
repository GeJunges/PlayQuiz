using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Produces("application/json")]
    [Route("api/questions")]
    public class QuestionsController : Controller
    {
        private readonly QuizContext _context;
        public QuestionsController(QuizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _context.Set<Question>();
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Question> Get([FromRoute] int quizId)
        {
            return _context.Set<Question>().Where(q => q.QuizId == quizId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            var quiz = _context.Set<Quiz>().SingleOrDefault(q => q.Id == question.QuizId);

            if(quiz == null)
            {
                return NotFound();
            }

            _context.Set<Question>().Add(question);
            await _context.SaveChangesAsync();
            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            var quiz = _context.Set<Quiz>().SingleOrDefault(q => q.Id == question.Id);

            if (quiz == null)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(question);
        }
    }
}