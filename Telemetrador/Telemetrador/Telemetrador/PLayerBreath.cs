using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class PLayerBreath : Event
    {
        public PLayerBreath(float timestamp)
        {
            setEventType(EventType.playerBreathes);
            data.Add("name", "Respira");
            data.Add("timestamp", timestamp.ToString());
        }

    }
}
