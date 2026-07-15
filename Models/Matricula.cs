using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models;

public class Matricula
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un estudiante.")]
    [Display(Name = "Estudiante")]
    public int EstudianteId { get; set; }

    [ForeignKey(nameof(EstudianteId))]
    public Estudiante? Estudiante { get; set; }

    [Required(ErrorMessage = "Debe seleccionar un curso.")]
    [Display(Name = "Curso")]
    public int CursoId { get; set; }

    [ForeignKey(nameof(CursoId))]
    public Curso? Curso { get; set; }

    [Required(ErrorMessage = "La fecha de matrícula es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de matrícula")]
    public DateTime FechaMatricula { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "El período académico es obligatorio.")]
    [StringLength(
        30,
        ErrorMessage = "El período académico no puede superar los 30 caracteres."
    )]
    [Display(Name = "Período académico")]
    public string PeriodoAcademico { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado de la matrícula es obligatorio.")]
    [StringLength(
        30,
        ErrorMessage = "El estado no puede superar los 30 caracteres."
    )]
    [Display(Name = "Estado")]
    public string Estado { get; set; } = "Activa";

    [Range(
        0,
        100,
        ErrorMessage = "La nota debe estar entre 0 y 100."
    )]
    [Display(Name = "Nota final")]
    public decimal? NotaFinal { get; set; }
}