﻿using System;
using System.Collections.Generic;

namespace Videojuegos.Infrastructura.Entities.Videojuegos;

public partial class UsuariosToken
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;
}
