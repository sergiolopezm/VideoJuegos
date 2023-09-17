using System;
using System.Collections.Generic;

namespace Videojuegos.Infrastructura.Entities.Videojuegos;

public partial class Videojuego
{
    public int VideojuegoId { get; set; }

    public string? Nombre { get; set; }

    public string? Compania { get; set; }

    public int? AnoLanzamiento { get; set; }

    public decimal? Precio { get; set; }

    public decimal? PuntajePromedio { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? Usuario { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();
}
