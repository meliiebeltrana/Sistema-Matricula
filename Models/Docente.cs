using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models;

public class Docente
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

    [Required(ErrorMessage = "La especialidad es obligatoria.")]
    [StringLength(
        100,
        ErrorMessage = "La especialidad no puede superar los 100 caracteres."
    )]
    [Display(Name = "Especialidad")]
    public string Especialidad { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
    [StringLength(
        20,
        ErrorMessage = "El teléfono no puede superar los 20 caracteres."
    )]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [Display(Name = "Docente activo")]
    public bool Activo { get; set; } = true;
}