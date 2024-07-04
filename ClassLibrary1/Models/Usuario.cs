using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Usuario
{
    public int IdUser { get; set; }

    public string? NameUser { get; set; }

    public string? SurnameUser { get; set; }

    public int? AgeUser { get; set; }

    public string? EmailUser { get; set; }

    public string? PassUser { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
