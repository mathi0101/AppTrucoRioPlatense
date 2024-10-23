using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucoRioPlatense.Models.Partida {
    internal class Partida {
        public VersionTruco Version { get; set; }

        public int NumeroJugadores {  get; set; }

        public JugadorPartidaTruco[] Jugadores { get; set; } 



    }
}
