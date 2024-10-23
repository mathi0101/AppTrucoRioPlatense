using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucoRioPlatense.Models.Partida {
    internal interface VersionTruco {

        string Nombre { get; }

        bool TieneMuestra { get; }


        int MaximoJugadores { get; }

    }
}
