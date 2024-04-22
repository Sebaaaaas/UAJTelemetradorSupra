using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class EndGame:Event
    {
        public EndGame(float timestamp, bool win)
        {
            setEventType(EventType.startGame);
            data.Add("timestamp", timestamp.ToString());
            data.Add("win", win.ToString());
        }
    }
}
