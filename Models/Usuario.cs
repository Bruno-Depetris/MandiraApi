using System;
using System.Collections.Generic;

namespace MandiraApi.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Rol { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
