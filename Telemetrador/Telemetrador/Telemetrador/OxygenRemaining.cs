using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelemetradorNamespace;

namespace TelemetradorNamespace
{
    public class OxygenRemaining : Event
    {
        public OxygenRemaining(float timestamp, float min, float sec, float mill)
        {
            setEventType(EventType.oxygenRemaining);
            data.Add("name", "Oxigeno que le queda");
            data.Add("timestamp", timestamp.ToString());
            data.Add("Cantidad de Oxigeno", min.ToString() + " " + sec.ToString() + " " + mill.ToString());
        }
    }
}
