using System;
using System.Collections.Generic;

namespace ClassLibrary1.models;

public partial class Articulo
{
    public int IdArt { get; set; }

    public string? NameArt { get; set; }

    public string? ImgArt { get; set; }

    public double? PriceArt { get; set; }

    public int? QuantityArt { get; set; }

    public string? DescriptionArt { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();
}
