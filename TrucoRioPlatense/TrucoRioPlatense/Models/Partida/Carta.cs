namespace TrucoRioPlatense.Models.Partida {
    internal class Carta {
        readonly int[] _numerosCarta = [1, 2, 3, 4, 5, 6, 7, 10, 11, 12];


        public int Numero { get; set; }

        public PaloCarta Palo { get; set; }


        public Carta(int numero, PaloCarta palo) {
            if (!_numerosCarta.Contains(numero)) throw new ExceptionNumeroCartaInvalido();
            Numero = numero;
            Palo = palo;
        }

        public override string ToString() {
            return $"Carta {Numero} de {Palo}";
        }



        public class ExceptionNumeroCartaInvalido : Exception { };
    }


    internal enum PaloCarta {
        Oro = 0,
        Copa = 1,
        Espada = 2,
        Basto = 3
    }
}
