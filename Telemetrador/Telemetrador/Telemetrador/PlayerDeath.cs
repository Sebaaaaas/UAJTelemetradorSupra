using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelemetradorNamespace;

namespace TelemetradorNamespace
{
    public class PlayerDeath : Event
    {
      
        public PlayerDeath(float timestamp,int numDeath)
        {
            setEventType(EventType.playerDeath);
            data.Add("name", "Muerte del jugador");
            data.Add("timestamp", timestamp.ToString());
            data.Add("NumMuertes", numDeath.ToString());
        }
    }
}
