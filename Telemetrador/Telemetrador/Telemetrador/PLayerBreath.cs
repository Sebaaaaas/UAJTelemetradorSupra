using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class PLayerBreath : Event
    {
        public PLayerBreath(float timestamp,float min,float sec, float mill)
        {
            setEventType(EventType.playerBreathes);
            data.Add("name", "Respira");
            data.Add("timestamp", timestamp.ToString());
            data.Add("reamainingTime",min.ToString()+" "+ sec.ToString()+" "+ mill.ToString());
        }

    }
}
