using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TelemetradorNamespace
{
    internal class SerializadorJSON:Serializador
    {
        public string serializa(Event ev)
        {
            
            string jsonString = JsonConvert.SerializeObject(ev.getData(),Formatting.Indented) ;
            return jsonString;
        }
        public string serializaTodo(ref Queue<Event> events)
        {
            string t="";
            while(events.Count > 0)
            {

                t += serializa(events.Dequeue());
                if(events.Count > 0 )
                {
                    t += ",";
                }
            }
            return t;
        }

        public string inicioSerializacion()
        {
            return "{\"datos\":[";
        }
        public string finSerializacion()
        {
            return "]}";
        }
        public Formatos getTipo()
        {
            return Formatos.JSON;
        }
        public string getExtension()
        {
            return ".json";
        }
    }
}
