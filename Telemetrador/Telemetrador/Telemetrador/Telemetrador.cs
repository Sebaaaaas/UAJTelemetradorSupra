using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    enum Events { startGame, endGame, playerBreathes, playerMoves};
    public class Telemetrador
    {
        private Queue<Events> events;
        private static Telemetrador instance = null;
        public Guid idSesion; //para guardar la sesion del usuario

        private Telemetrador()
        {
            events = new Queue<Events>();
            idSesion = Guid.NewGuid();

            events.Append(Events.startGame);

        }
        public static Telemetrador Instance()
        {
            return instance;
        }

        public static bool Inicializacion(string nombreJuego_)
        {
            if (instance != null) return false;

            instance = new Telemetrador();
            return true;
        }
    }
}
