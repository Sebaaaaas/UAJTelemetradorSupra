using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetrador
{
    enum Events { startGame, endGame};
    public class Telemetrador
    {
        private Queue<Events> events;
        private static Telemetrador instance = null;

        private Telemetrador()
        {
            events = new Queue<Events>();
        }
        public Telemetrador Instance()
        {
            return instance;
        }

        public static bool Inicializacion()
        {
            if (instance != null) return false;

            instance = new Telemetrador();
            return true;
        }
    }
}
