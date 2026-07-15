using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[Authorize(Roles = "Administrador")]
public class EstudiantesController : Controller
{
    private readonly ApplicationDbContext _context;

    public EstudiantesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Estudiantes
    public async Task<IActionResult> Index()
    {
        var estudiantes = await _context.Estudiantes
            .Include(e => e.Carrera)
            .OrderBy(e => e.NombreCompleto)
            .ToListAsync();

        return View(estudiantes);
    }

    // GET: Estudiantes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return View(estudiante);
    }

    // GET: Estudiantes/Create
    public async Task<IActionResult> Create()
    {
        await CargarCarrerasAsync();

        return View();
    }

    // POST: Estudiantes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "Identificacion,NombreCompleto,Correo,Telefono," +
            "FechaNacimiento,CarreraId,Activo"
        )]
        Estudiante estudiante)
    {
        if (ModelState.IsValid)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El estudiante se registró correctamente.";

            return RedirectToAction(nameof(Index));
        }

        await CargarCarrerasAsync(estudiante.CarreraId);

        return View(estudiante);
    }

    // GET: Estudiantes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante == null)
        {
            return NotFound();
        }

        await CargarCarrerasAsync(estudiante.CarreraId);

        return View(estudiante);
    }

    // POST: Estudiantes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind(
            "Id,Identificacion,NombreCompleto,Correo,Telefono," +
            "FechaNacimiento,CarreraId,Activo"
        )]
        Estudiante estudiante)
    {
        if (id != estudiante.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Estudiantes.Update(estudiante);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "El estudiante se actualizó correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudianteExists(estudiante.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        await CargarCarrerasAsync(estudiante.CarreraId);

        return View(estudiante);
    }

    // GET: Estudiantes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estudiante = await _context.Estudiantes
            .Include(e => e.Carrera)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (estudiante == null)
        {
            return NotFound();
        }

        return View(estudiante);
    }

    // POST: Estudiantes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var estudiante = await _context.Estudiantes.FindAsync(id);

        if (estudiante != null)
        {
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El estudiante se eliminó correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarCarrerasAsync(
        int? carreraSeleccionada = null)
    {
        var carreras = await _context.Carreras
            .Where(c => c.Activa)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        ViewData["CarreraId"] = new SelectList(
            carreras,
            "Id",
            "Nombre",
            carreraSeleccionada
        );
    }

    private bool EstudianteExists(int id)
    {
        return _context.Estudiantes.Any(e => e.Id == id);
    }
}