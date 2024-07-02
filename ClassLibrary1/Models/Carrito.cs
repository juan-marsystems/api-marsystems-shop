using System;
using System.Collections.Generic;

namespace ClassLibrary1.models;

public partial class Carrito
{
    public int IdCart { get; set; }

    public int? IdUser { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Usuario? IdUserNavigation { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
