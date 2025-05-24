using System;
using System.Collections.Generic;

namespace MandiraApi.Models;

public partial class Ordene
{
    public int OrdenId { get; set; }

    public int? UsuarioId { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; } = new List<DetalleOrden>();

    public virtual Usuario? Usuario { get; set; }
}
