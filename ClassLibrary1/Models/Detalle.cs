using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClassLibrary1.Models;

public partial class Detalle
{
    public int IdDetail { get; set; }

    public int? IdCart { get; set; }

    public int? IdArt { get; set; }

    public int? QuantityDetail { get; set; }

    [JsonIgnore]
    public virtual Articulo? IdArtNavigation { get; set; }

    [JsonIgnore]
    public virtual Carrito? IdCartNavigation { get; set; }
}
