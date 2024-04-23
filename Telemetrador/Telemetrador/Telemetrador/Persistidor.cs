using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    abstract class Persistidor
    {
        protected Serializador serializador;
        protected Queue<Event> events;
        protected Persistidor(Serializador s)
        {
            serializador = s;
            events = new Queue<Event>();
        }

        public void addEvent(Event e)
        {
            events.Enqueue(e);
        }
        public void removeEvent() {
            if (events.Count > 0)
            {
                events.Dequeue();
            }
        }
        public abstract void flush();
        public abstract void close();
        
        
    }
}
