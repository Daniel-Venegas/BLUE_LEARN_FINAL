﻿using System.ComponentModel.DataAnnotations;

namespace AppLogin.Model
{
    public class Jugador
    {

        [Key]
        public int IdJugador { get; set; }
        public required int Puntaje { get; set; }
        public required int Nivel { get; set; }
        public bool Eliminado { get; set; }
    }
}
