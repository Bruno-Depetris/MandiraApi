using System;
using System.Collections.Generic;

namespace MandiraApi.Models;

public partial class ImagenesProducto
{
    public int ImagenProductoId { get; set; }

    public int? ProductoId { get; set; }

    public string Url { get; set; } = null!;

    public byte[]? Imagen { get; set; }

    public virtual Producto? Producto { get; set; }
}
