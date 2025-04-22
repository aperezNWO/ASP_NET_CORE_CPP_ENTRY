using System;
using System.Collections.Generic;

namespace Pruebas.Cliente.Models;

public partial class Tercero
{
    public int IdPersona { get; set; }

    public string Registro { get; set; } = null!;

    public int IdPais { get; set; }

    public bool FPublica { get; set; }

    public int? IdFuerza { get; set; }

    public int? IdJefatura { get; set; }

    public int? IdComando { get; set; }

    public int? IdDivision { get; set; }

    public int? IdBrigada { get; set; }

    public int? IdBatallon { get; set; }

    public int? IdCompañia { get; set; }

    public int? IdPeloton { get; set; }

    public int? IdComandante { get; set; }

    public int? IdSuperior { get; set; }

    public int? IdSeccion { get; set; }

    public int? IdCargo { get; set; }

    public int? IdCargoA { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdGrado { get; set; }

    public int? IdCuerpo { get; set; }

    public int? IdArma { get; set; }

    public bool Reserva { get; set; }

    public string Cedula { get; set; } = null!;

    public string PApellido { get; set; } = null!;

    public string? SApellido { get; set; }

    public string PNombre { get; set; } = null!;

    public string? SNombre { get; set; }

    public int IdGenero { get; set; }

    public DateOnly? FNacimiento { get; set; }

    public int IdRh { get; set; }

    public string? Celular { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Titular { get; set; }

    public string? Zona { get; set; }

    public string? Sector { get; set; }

    public int? IdCasa { get; set; }

    public int? Tipo { get; set; }

    public string? Torre { get; set; }

    public string? Piso { get; set; }

    public string? Placa { get; set; }

    public DateOnly Creado { get; set; }

    public bool Estado { get; set; }

    public string? Huella { get; set; }

    public byte[]? Foto { get; set; }

    public string? Observaciones { get; set; }
}
