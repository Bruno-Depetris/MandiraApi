using System;
using System.Collections.Generic;

namespace MandiraApi.Models;

public partial class VistaProductosActivo
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public string? ImagenUrl { get; set; }

    public string Categoria { get; set; } = null!;
}
