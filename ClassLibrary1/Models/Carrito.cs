using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClassLibrary1.Models;

public partial class Carrito
{
    public int IdCart { get; set; }

    public int? IdUser { get; set; }

    public bool? StatusCart { get; set; }

    [JsonIgnore]
    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    [JsonIgnore]
    public virtual Usuario? IdUserNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
