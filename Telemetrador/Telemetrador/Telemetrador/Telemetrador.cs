using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class Telemetrador
    {
        private Queue<Event> events;
        private static Telemetrador instance = null;
        private int eventIDcounter;
        public Guid idSesion; //para guardar la sesion del usuario

        private Telemetrador()
        {
            events = new Queue<Event>();
            idSesion = Guid.NewGuid();
            eventIDcounter = 0;
        }
        public static Telemetrador Instance()
        {
            return instance;
        }

        public static bool Init()
        {
            if (instance != null) return false;

            instance = new Telemetrador();
            return true;
        }

        public void startSession(float timestamp, string nombreJuego_)
        {
            StartGame ev = new StartGame(timestamp, nombreJuego_);
            ev.setID(eventIDcounter++);
            events.Append(ev);
        }
        public void endSession(float timestamp, bool win)
        {
            EndGame ev = new EndGame(timestamp, win);
            ev.setID(eventIDcounter++);
            events.Append(ev);
        }
        public void addEvent(Event ev)
        {
            ev.setID(eventIDcounter++);
            events.Append(ev);
        }
    }
}
