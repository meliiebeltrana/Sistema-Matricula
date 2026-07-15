using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[Authorize(Roles = "Administrador")]
public class CursosController : Controller
{
    private readonly ApplicationDbContext _context;

    public CursosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Cursos
    public async Task<IActionResult> Index()
    {
        var cursos = await _context.Cursos
            .Include(c => c.Carrera)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        return View(cursos);
    }

    // GET: Cursos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Carrera)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // GET: Cursos/Create
    public async Task<IActionResult> Create()
    {
        await CargarCarrerasAsync();

        return View();
    }

    // POST: Cursos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Codigo,Nombre,Creditos,CupoMaximo,Activo,CarreraId")]
        Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El curso se registró correctamente.";

            return RedirectToAction(nameof(Index));
        }

        await CargarCarrerasAsync(curso.CarreraId);

        return View(curso);
    }

    // GET: Cursos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos.FindAsync(id);

        if (curso == null)
        {
            return NotFound();
        }

        await CargarCarrerasAsync(curso.CarreraId);

        return View(curso);
    }

    // POST: Cursos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Codigo,Nombre,Creditos,CupoMaximo,Activo,CarreraId")]
        Curso curso)
    {
        if (id != curso.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Cursos.Update(curso);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "El curso se actualizó correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(curso.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        await CargarCarrerasAsync(curso.CarreraId);

        return View(curso);
    }

    // GET: Cursos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var curso = await _context.Cursos
            .Include(c => c.Carrera)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
        {
            return NotFound();
        }

        return View(curso);
    }

    // POST: Cursos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var curso = await _context.Cursos.FindAsync(id);

        if (curso != null)
        {
            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El curso se eliminó correctamente.";
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

    private bool CursoExists(int id)
    {
        return _context.Cursos.Any(c => c.Id == id);
    }
}