using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ResumeManager.Data;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly AppDbContext _context;

        public CandidatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Candidates.Include(c => c.Degree);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Degree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Candidate candidate, IFormFile? CvUpload)
        {
            if (ModelState.IsValid)
            {
                if (CvUpload != null && CvUpload.Length > 0)
                {
                    var fileName = Path.GetFileName(CvUpload.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await CvUpload.CopyToAsync(stream);
                    }

                    candidate.CvFilePath = "/uploads/" + uniqueFileName;
                }

                _context.Add(candidate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", candidate.DegreeId);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", candidate.DegreeId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Candidate candidate, IFormFile? CvUpload)
        {
            // Clear any messages that shouldn't appear on edit page
            TempData.Clear();

            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", candidate.DegreeId);
                return View(candidate);
            }

            var existingCandidate = await _context.Candidates.FindAsync(id);
            if (existingCandidate == null)
            {
                return NotFound();
            }

            // Update fields
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.Email = candidate.Email;
            existingCandidate.Mobile = candidate.Mobile;
            existingCandidate.DegreeId = candidate.DegreeId;
            existingCandidate.CreationTime = candidate.CreationTime;

            // Handle file upload
            if (CvUpload != null && CvUpload.Length > 0)
            {
                var fileName = Path.GetFileName(CvUpload.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder); // Make sure folder exists
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CvUpload.CopyToAsync(stream);
                }

                // Optionally delete old file
                if (!string.IsNullOrEmpty(existingCandidate.CvFilePath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCandidate.CvFilePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existingCandidate.CvFilePath ="/uploads/" + uniqueFileName;
            }

              await _context.SaveChangesAsync();
              TempData["Message"] = "Candidate updated successfully.";
            

            return RedirectToAction(nameof(Index), new { id = candidate.Id });
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidates
                .Include(c => c.Degree)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCv(int id, string returnUrl)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null || string.IsNullOrEmpty(candidate.CvFilePath))
            {
                return RedirectToAction(nameof(Index));
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", candidate.CvFilePath.TrimStart('/'));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            candidate.CvFilePath = null;
            _context.Update(candidate);
            await _context.SaveChangesAsync();

            TempData["DeleteMessage"] = "The CV file deleted successfully.";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index), new { id = candidate.Id });
        }



        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }
    }
}
