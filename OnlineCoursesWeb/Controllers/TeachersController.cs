using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesWeb.Data;
using OnlineCoursesWeb.Models;

namespace OnlineCoursesWeb.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        [Authorize]
        public async Task<IActionResult> Index(string Language, string Course, string Level)
        {
            TeacherSearch teacherSearch = new TeacherSearch();
            teacherSearch.Teachers = await _context.Teacher.ToListAsync();
            teacherSearch.Courses = new SelectList(await _context.Teacher.Select(x => x.Course).Distinct().ToListAsync());
            teacherSearch.Languages = new SelectList(await _context.Teacher.Select(x => x.Language).Distinct().ToListAsync());
            teacherSearch.Levels = new SelectList(await _context.Teacher.Select(x => x.Level).Distinct().ToListAsync());

            var data = _context.Teacher.Select(x => x);

            if (!string.IsNullOrEmpty(Language))
            {
                data = data.Where(x => x.Language.Contains(Language));
            }
            if (!string.IsNullOrEmpty(Course))
            {
                data = data.Where(x => x.Course.Contains(Course));
            }
            if (!string.IsNullOrEmpty(Level))
            {
                data = data.Where(x => x.Level.Contains(Level));
            }

            teacherSearch.Teachers = await data.ToListAsync();
            return View(teacherSearch);

        }
        // GET: Teachers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        [Authorize(Roles = "Admin, Teacher")]
        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> Create([Bind("Id,Name,Language,Course,Level")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
        [Authorize]
        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        [Authorize]
        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Language,Course,Level")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
        [Authorize]
        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        [Authorize]
        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.Id == id);
        }
    }
}
