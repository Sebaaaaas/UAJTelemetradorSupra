using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class PlayerAscension : Event
    {
        public PlayerAscension(float timestamp, float ascensionStartTimestamp, bool drowned)
        {
            setEventType(EventType.playerBreathes);
            data.Add("name", "Ascenso_");
            data.Add("timestamp", timestamp.ToString());
            data.Add("ascensionStartTimestamp", ascensionStartTimestamp.ToString());
            data.Add("drowned", drowned.ToString());
        }
    }
}
