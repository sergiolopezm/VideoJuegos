using System;
using System.Collections.Generic;

namespace Videojuegos.Infrastructura.Entities.Videojuegos;

public partial class Calificacione
{
    public Guid CalificacionId { get; set; }

    public string? NicknameJugador { get; set; }

    public int? VideojuegoId { get; set; }

    public decimal? Puntuacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public string? Usuario { get; set; }

    public virtual Videojuego? Videojuego { get; set; }
}
