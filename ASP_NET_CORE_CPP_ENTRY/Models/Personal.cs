using System;
using System.Collections.Generic;

namespace Pruebas.Cliente.Models;

public partial class Personal
{
    public int Id { get; set; }

    public string Registro { get; set; } = null!;

    public int IdCategoria { get; set; }

    public int IdGrado { get; set; }

    public int IdCuerpo { get; set; }

    public int IdArma { get; set; }

    public string Cedula { get; set; } = null!;

    public string PApellido { get; set; } = null!;

    public string? SApellido { get; set; }

    public string PNombre { get; set; } = null!;

    public string? SNombre { get; set; }

    public string IdGenero { get; set; } = null!;

    public DateOnly FNacimiento { get; set; }

    public string IdRh { get; set; } = null!;

    public byte[]? Huella1 { get; set; }

    public byte[]? Huella2 { get; set; }

    public byte[]? Foto { get; set; }

    public DateOnly Creado { get; set; }

    public bool Estado { get; set; }
}
