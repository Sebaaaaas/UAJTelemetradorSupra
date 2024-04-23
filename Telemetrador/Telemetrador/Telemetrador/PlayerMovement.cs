using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    public class PlayerMovement : Event
    {
        public PlayerMovement(float timestamp, float posX, float posY)
        {
            setEventType(EventType.playerPosition);
            data.Add("timestamp", timestamp.ToString());
            data.Add("playerX", posX.ToString());
            data.Add("playerY", posY.ToString());
        }
    }
}
