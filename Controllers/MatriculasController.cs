using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[Authorize(Roles = "Administrador")]
public class MatriculasController : Controller
{
    private readonly ApplicationDbContext _context;

    public MatriculasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Matriculas
    public async Task<IActionResult> Index()
    {
        var matriculas = await _context.Matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
                .ThenInclude(c => c!.Carrera)
            .OrderByDescending(m => m.FechaMatricula)
            .ToListAsync();

        return View(matriculas);
    }

    // GET: Matriculas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula = await _context.Matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
                .ThenInclude(c => c!.Carrera)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // GET: Matriculas/Create
    public async Task<IActionResult> Create()
    {
        await CargarListasAsync();

        return View(new Matricula
        {
            FechaMatricula = DateTime.Today,
            Estado = "Activa"
        });
    }

    // POST: Matriculas/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "EstudianteId,CursoId,FechaMatricula," +
            "PeriodoAcademico,Estado,NotaFinal"
        )]
        Matricula matricula)
    {
        bool matriculaDuplicada =
            await _context.Matriculas.AnyAsync(
                m => m.EstudianteId == matricula.EstudianteId
                     && m.CursoId == matricula.CursoId
                     && m.PeriodoAcademico ==
                        matricula.PeriodoAcademico
            );

        if (matriculaDuplicada)
        {
            ModelState.AddModelError(
                string.Empty,
                "El estudiante ya está matriculado en este curso " +
                "durante el período seleccionado."
            );
        }

        if (ModelState.IsValid)
        {
            _context.Matriculas.Add(matricula);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "La matrícula se registró correctamente.";

            return RedirectToAction(nameof(Index));
        }

        await CargarListasAsync(
            matricula.EstudianteId,
            matricula.CursoId
        );

        return View(matricula);
    }

    // GET: Matriculas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula =
            await _context.Matriculas.FindAsync(id);

        if (matricula == null)
        {
            return NotFound();
        }

        await CargarListasAsync(
            matricula.EstudianteId,
            matricula.CursoId
        );

        return View(matricula);
    }

    // POST: Matriculas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind(
            "Id,EstudianteId,CursoId,FechaMatricula," +
            "PeriodoAcademico,Estado,NotaFinal"
        )]
        Matricula matricula)
    {
        if (id != matricula.Id)
        {
            return NotFound();
        }

        bool matriculaDuplicada =
            await _context.Matriculas.AnyAsync(
                m => m.Id != matricula.Id
                     && m.EstudianteId ==
                        matricula.EstudianteId
                     && m.CursoId == matricula.CursoId
                     && m.PeriodoAcademico ==
                        matricula.PeriodoAcademico
            );

        if (matriculaDuplicada)
        {
            ModelState.AddModelError(
                string.Empty,
                "El estudiante ya está matriculado en este curso " +
                "durante el período seleccionado."
            );
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Matriculas.Update(matricula);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "La matrícula se actualizó correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatriculaExists(matricula.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        await CargarListasAsync(
            matricula.EstudianteId,
            matricula.CursoId
        );

        return View(matricula);
    }

    // GET: Matriculas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var matricula = await _context.Matriculas
            .Include(m => m.Estudiante)
            .Include(m => m.Curso)
                .ThenInclude(c => c!.Carrera)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (matricula == null)
        {
            return NotFound();
        }

        return View(matricula);
    }

    // POST: Matriculas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var matricula =
            await _context.Matriculas.FindAsync(id);

        if (matricula != null)
        {
            _context.Matriculas.Remove(matricula);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "La matrícula se eliminó correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CargarListasAsync(
        int? estudianteSeleccionado = null,
        int? cursoSeleccionado = null)
    {
        var estudiantes = await _context.Estudiantes
            .Where(e => e.Activo)
            .OrderBy(e => e.NombreCompleto)
            .ToListAsync();

        var cursosBase = await _context.Cursos
            .Where(c => c.Activo)
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        var cursos = cursosBase.Select(c => new
        {
            c.Id,
            Texto = $"{c.Codigo} - {c.Nombre}"
        });

        ViewData["EstudianteId"] = new SelectList(
            estudiantes,
            "Id",
            "NombreCompleto",
            estudianteSeleccionado
        );

        ViewData["CursoId"] = new SelectList(
            cursos,
            "Id",
            "Texto",
            cursoSeleccionado
        );
    }

    private bool MatriculaExists(int id)
    {
        return _context.Matriculas.Any(m => m.Id == id);
    }
}