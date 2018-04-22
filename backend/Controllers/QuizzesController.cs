using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace backend.Controllers
{
    [Produces("application/json")]
    [Route("api/quizzes")]
    public class QuizzesController : Controller
    {
        private readonly QuizContext _context;

        public QuizzesController(QuizContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Quiz> Get()
        {
            var userId = HttpContext.User.Claims.First().Value;

            return _context.Set<Quiz>().Where(u => u.OwnerId == new Guid(userId));
        }

        [HttpGet("all")]
        public IEnumerable<Quiz> GetAllQuizzes()
        {
            return _context.Set<Quiz>();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = await _context.Set<Quiz>().SingleOrDefaultAsync(m => m.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid )
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.Claims.First().Value;
            quiz.OwnerId = new Guid(userId);

            _context.Set<Quiz>().Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = await _context.Set<Quiz>().SingleOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Set<Quiz>().Remove(quiz);
            await _context.SaveChangesAsync();

            return Ok(quiz);
        }

        private bool QuizExists(int id)
        {
            return _context.Set<Quiz>().Any(e => e.Id == id);
        }
    }
}