using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class StartGame : Event
    {
        public StartGame(float timestamp, string gameName)
        {
            setEventType(EventType.startGame);
            data.Add("name", "Empiece_Partida");
            data.Add("timestamp", timestamp.ToString());
            data.Add("gameName", gameName);
        }
    }
}
