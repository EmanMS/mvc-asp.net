using clinic_Manage.Data;
using clinic_Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AppointmentsController : Controller
{
    private readonly ClinicDbContext _context;

    public AppointmentsController(ClinicDbContext context)
    {
        _context = context;
    }

    public ActionResult Appointments()
    {

        return View();
    }

    // GET: Appointments
    public async Task<IActionResult> Index()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .ToListAsync();
        return View(appointments);
    }

    // GET: Appointments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(m => m.AppointmentId == id);
        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // GET: Appointments/Create
    public IActionResult Create()
    {
        ViewBag.DoctorSelectList = new SelectList(_context.Doctors, "DoctorId", "Name");
        ViewBag.PatientSelectList = new SelectList(_context.Patients, "PatientId", "Name");
        return View();
    }

    // POST: Appointments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AppointmentId,DoctorId,PatientId,AppointmentDate ,ReasonForVisit,Cost,Status")] Appointment appointment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.DoctorSelectList = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
        ViewBag.PatientSelectList = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
        return View(appointment);
    }

    // GET: Appointments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }
        ViewBag.DoctorSelectList = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
        ViewBag.PatientSelectList = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
        return View(appointment);
    }

    // POST: Appointments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,DoctorId,PatientId,AppointmentDate")] Appointment appointment)
    {
        if (id != appointment.AppointmentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(appointment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(appointment.AppointmentId))
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
        ViewBag.DoctorSelectList = new SelectList(_context.Doctors, "DoctorId", "Name", appointment.DoctorId);
        ViewBag.PatientSelectList = new SelectList(_context.Patients, "PatientId", "Name", appointment.PatientId);
        return View(appointment);
    }

    // GET: Appointments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(m => m.AppointmentId == id);
        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    // POST: Appointments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AppointmentExists(int id)
    {
        return _context.Appointments.Any(e => e.AppointmentId == id);
    }
}