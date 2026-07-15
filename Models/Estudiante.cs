using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models;

public class Estudiante
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [StringLength(
        25,
        ErrorMessage = "La identificación no puede superar los 25 caracteres."
    )]
    [Display(Name = "Identificación")]
    public string Identificacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(
        120,
        ErrorMessage = "El nombre no puede superar los 120 caracteres."
    )]
    [Display(Name = "Nombre completo")]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
    [StringLength(
        150,
        ErrorMessage = "El correo no puede superar los 150 caracteres."
    )]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Debe ingresar un teléfono válido.")]
    [StringLength(
        20,
        ErrorMessage = "El teléfono no puede superar los 20 caracteres."
    )]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una carrera.")]
    [Display(Name = "Carrera")]
    public int CarreraId { get; set; }

    [ForeignKey(nameof(CarreraId))]
    public Carrera? Carrera { get; set; }

    [Display(Name = "Estudiante activo")]
    public bool Activo { get; set; } = true;
}