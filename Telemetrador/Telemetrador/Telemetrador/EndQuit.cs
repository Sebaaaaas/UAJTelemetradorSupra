using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelemetradorNamespace;

namespace TelemetradorNamespace
{
    public class EndQuit :Event
    {
        public EndQuit(float timestamp)
        {
            setEventType(EventType.endQuit);
            data.Add("name", "Salida_Jugador");
            data.Add("timestamp", timestamp.ToString());
            
        }
    }
}
