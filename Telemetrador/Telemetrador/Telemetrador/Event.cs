using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{

    public enum EventType { startGame, endGame, playerBreathes, playerPosition, playerAscending,endQuit,startLevel };


    public class Event
    {
        protected Dictionary<string, string> data;
        EventType eType;
        int eventID;
        public Event()
        {
            data=new Dictionary<string, string>();
        }
        protected void setEventType(EventType type)
        {
            eType = type;
        }
        public EventType getType()
        {
            return eType;
        }
        public Dictionary<string, string> getData()
        {
            return data;
        }
        public void setID(int id)
        {
            eventID = id;
        }
        public int getID()
        {
            return eventID;
        }
    }
}
