using System;
using System.Collections.Generic;

namespace MandiraApi.Models;

public partial class Carrito
{
    public int CarritoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? ProductoId { get; set; }

    public int Cantidad { get; set; }

    public DateTime? AgregadoEn { get; set; }

    public virtual Producto? Producto { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
