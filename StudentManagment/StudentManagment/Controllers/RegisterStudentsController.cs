using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagment.Data;
using StudentManagment.Models;
using cloudscribe.Pagination.Models;

namespace StudentManagment.Controllers
{
    [Authorize(Roles = "Admin,Executive")]
    public class RegisterStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RegisterStudents
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber=1, int pageSize=4)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.FirstNameSortParam = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            int ExludeRecords = (pageSize * pageNumber) - pageSize;
            var applicationDbContext = from r in _context.RegisterStudents.Include(r => r.Batch).Include(r => r.Course)
                                       select r;

            var StudentCount = applicationDbContext.Count();

            if(!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(r => r.FirstName.Contains(searchString) || r.LastName.Contains(searchString));
                StudentCount = applicationDbContext.Count();
            }

            //Sort logic
            switch(sortOrder)
            {
                case "FirstName_desc":
                    applicationDbContext = applicationDbContext.OrderByDescending(r => r.FirstName);
                    break;
                default:
                    applicationDbContext = applicationDbContext.OrderBy(r => r.FirstName);
                    break;
            }

            applicationDbContext=applicationDbContext
                .Skip(ExludeRecords)
                .Take(pageSize);

            var result = new PagedResult<RegisterStudent>
            {
                Data = await applicationDbContext.AsNoTracking().ToListAsync(),
                TotalItems = StudentCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }

        // GET: RegisterStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerStudent = await _context.RegisterStudents
                .Include(r => r.Batch)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(m => m.id == id);
            if (registerStudent == null)
            {
                return NotFound();
            }

            return View(registerStudent);
        }

        // GET: RegisterStudents/Create
        public IActionResult Create()
        {
            ViewData["Batchid"] = new SelectList(_context.Batches, "id", "BatchName");
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "CourseName");
            return View();
        }

        // POST: RegisterStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName,Nic,Courseid,Batchid,Email,Phone")] RegisterStudent registerStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registerStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Batchid"] = new SelectList(_context.Batches, "id", "BatchName", registerStudent.Batchid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "CourseName", registerStudent.Courseid);
            return View(registerStudent);
        }

        // GET: RegisterStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerStudent = await _context.RegisterStudents.FindAsync(id);
            if (registerStudent == null)
            {
                return NotFound();
            }
            ViewData["Batchid"] = new SelectList(_context.Batches, "id", "BatchName", registerStudent.Batchid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "CourseName", registerStudent.Courseid);
            return View(registerStudent);
        }

        // POST: RegisterStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName,Nic,Courseid,Batchid,Email,Phone")] RegisterStudent registerStudent)
        {
            if (id != registerStudent.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterStudentExists(registerStudent.id))
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
            ViewData["Batchid"] = new SelectList(_context.Batches, "id", "BatchName", registerStudent.Batchid);
            ViewData["Courseid"] = new SelectList(_context.Courses, "Id", "CourseName", registerStudent.Courseid);
            return View(registerStudent);
        }

        [Authorize(Roles = "Admin")]
        // GET: RegisterStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerStudent = await _context.RegisterStudents
                .Include(r => r.Batch)
                .Include(r => r.Course)
                .FirstOrDefaultAsync(m => m.id == id);
            if (registerStudent == null)
            {
                return NotFound();
            }

            return View(registerStudent);
        }

        // POST: RegisterStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerStudent = await _context.RegisterStudents.FindAsync(id);
            _context.RegisterStudents.Remove(registerStudent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterStudentExists(int id)
        {
            return _context.RegisterStudents.Any(e => e.id == id);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index2(string searchString, string sortOrder, int pageNumber = 1, int pageSize = 4)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.FirstNameSortParam = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            int ExludeRecords = (pageSize * pageNumber) - pageSize;
            var applicationDbContext = from r in _context.RegisterStudents.Include(r => r.Batch).Include(r => r.Course)
                                       select r;

            var StudentCount = applicationDbContext.Count();

            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = applicationDbContext.Where(r => r.FirstName.Contains(searchString) || r.LastName.Contains(searchString));
                StudentCount = applicationDbContext.Count();
            }

            //Sort logic
            switch (sortOrder)
            {
                case "FirstName_desc":
                    applicationDbContext = applicationDbContext.OrderByDescending(r => r.FirstName);
                    break;
                default:
                    applicationDbContext = applicationDbContext.OrderBy(r => r.FirstName);
                    break;
            }

            applicationDbContext = applicationDbContext
                .Skip(ExludeRecords)
                .Take(pageSize);

            var result = new PagedResult<RegisterStudent>
            {
                Data = await applicationDbContext.AsNoTracking().ToListAsync(),
                TotalItems = StudentCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(result);
        }
    }
}
