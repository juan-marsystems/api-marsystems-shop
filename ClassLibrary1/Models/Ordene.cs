using System;
using System.Collections.Generic;

namespace ClassLibrary1.models;

public partial class Ordene
{
    public int IdOrder { get; set; }

    public int? IdUser { get; set; }

    public int? IdCart { get; set; }

    public DateTime? DateOrder { get; set; }

    public double? TotalOrder { get; set; }

    public virtual Carrito? IdCartNavigation { get; set; }

    public virtual Usuario? IdUserNavigation { get; set; }
}
