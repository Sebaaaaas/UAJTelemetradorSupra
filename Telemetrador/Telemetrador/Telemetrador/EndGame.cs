using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class EndGame : Event
    {
        public EndGame(float timestamp, bool win, float min, float sec, float mill)
        {
            setEventType(EventType.endGame);
            data.Add("name", "Fin");
            data.Add("timestamp", timestamp.ToString());
            data.Add("win", win.ToString());
            if (win)
            {
                data.Add("Tiempo en encontrarlo", min.ToString() + " " + sec.ToString() + " " + mill.ToString());
            }
        }
    }
}
