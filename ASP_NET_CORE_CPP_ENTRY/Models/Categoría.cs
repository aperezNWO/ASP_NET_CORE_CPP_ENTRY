using System;
using System.Collections.Generic;

namespace Pruebas.Cliente.Models;

public partial class Categoría
{
    public int Id { get; set; }

    public string? Categoria { get; set; }

    public bool? Estado { get; set; }
}
