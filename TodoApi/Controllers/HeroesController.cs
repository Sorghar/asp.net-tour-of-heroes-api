using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroContext _context;

        private readonly Hero[] _defaultHeroes = { new Hero { Id = 11, Name = "Dr Nice" },
                                                   new Hero { Id = 12, Name = "Narco" },
                                                   new Hero { Id = 13, Name = "Bombasto" },
                                                   new Hero { Id = 14, Name = "Celeritas" },
                                                   new Hero { Id = 15, Name = "Magneta" },
                                                   new Hero { Id = 16, Name = "RubberMan" },
                                                   new Hero { Id = 17, Name = "Dynama" },
                                                   new Hero { Id = 18, Name = "Dr IQ" },
                                                   new Hero { Id = 19, Name = "Magma" },
                                                   new Hero { Id = 20, Name = "Tornado" }
                                                 };

        /// <summary>
        /// Constructor, code inside runs for every new http request
        /// </summary>
        public HeroesController(HeroContext context)
        {
            _context = context;

            if (_context.Heroes.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Heroes.AddRange(this._defaultHeroes);
                _context.SaveChanges();
            }
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hero>>> GetHeroes()
        {
            return await _context.Heroes.ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(long id)
        {
            Hero hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        // POST: api/Heroes
        [HttpPost]
        public async Task<ActionResult<Hero>> PostTodoItem(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHero), new { id = hero.Id }, hero);
        }

        // PUT: api/Heroes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(long id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(long id)
        {
            Hero hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
