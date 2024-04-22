using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class PlayerAscension:Event
    {
        public PlayerAscension(float timestamp, bool isAscending)
        {
            setEventType(EventType.playerBreathes);
            data.Add("timestamp", timestamp.ToString());
            data.Add("isAscending", isAscending.ToString());
        }
    }
}
