using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using SchoolManagementSystem.Models;

public class AccountSummaryController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountSummaryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Fetch tuition billing details for a student
    public async Task<IActionResult> TuitionBilling(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
        {
            return BadRequest("Student ID is required.");
        }

        // Fetch student details
        var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == int.Parse(studentId));
        if (student == null)
        {
            return NotFound("Student not found.");
        }

        // Fetch tuition billing and payments
        var billing = await _context.TuitionBillings
            .Include(tb => tb.TuitionPayments)
            .FirstOrDefaultAsync(tb => tb.StudentId == int.Parse(studentId)); 

        if (billing == null)
        {
            return NotFound("Tuition billing information not found.");
        }

        ViewData["Student"] = student;
        return View(billing);
    }
}
