using System;
using System.Collections.Generic;

namespace ClassLibrary1.models;

public partial class Detalle
{
    public int IdDetail { get; set; }

    public int? IdCart { get; set; }

    public int? IdArt { get; set; }

    public int? QuantityDetail { get; set; }

    public virtual Articulo? IdArtNavigation { get; set; }

    public virtual Carrito? IdCartNavigation { get; set; }
}
