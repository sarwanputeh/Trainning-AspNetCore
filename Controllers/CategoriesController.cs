using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrixNetCoreApp.Data;
using OrixNetCoreApp.Models;

namespace OrixNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly APIContext _context;

        public CategoriesController(APIContext context)
        {
            _context = context;
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories(int page =1,int pageSize = 3)
        {
            var categories = await _context.Categories
                .Select(c => new { c.CategoryId, c.CategoryName })
                .OrderByDescending(c=> c.CategoryId)
                 .Skip((page - 1)* pageSize)
                 .Take(pageSize)
                .OrderByDescending(c => c.CategoryId)
                .AsNoTracking()
                .ToListAsync();

            //SQL
            //var categories = await _context.Categories
            //     .FromSqlRaw("select * from Categories order by CategoryId desc").ToListAsync();

            //count record
            var total = await _context.Categories.CountAsync();

            return Ok(new {
                totalRecord = total, 
                data = categories
            });
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
        
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new {meassage ="ไม่พบข้อมูลนี้ในระบบ"});
            }
           
            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, new { message = "บันทึกข้อมูลสำเร็จ", data = category } );
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(new { meassage = "ไม่พบข้อมูลให้ลบ" });
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
