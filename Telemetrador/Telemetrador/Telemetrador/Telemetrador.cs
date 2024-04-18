﻿using System;
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
        public Guid idSesion; //para guardar la sesion del usuario

        private Telemetrador()
        {
            events = new Queue<Events>();
            idSesion = Guid.NewGuid();

            events.Append(Events.startGame);

        }
        public Telemetrador Instance()
        {
            return instance;
        }

        public static bool Inicializacion(string nombreJuego_, long idSesion_)
        {
            if (instance != null) return false;

            instance = new Telemetrador();
            return true;
        }
    }
}
