using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TelemetradorNamespace
{
    internal class SerializadorJSON:Serialiador
    {
        public string serializa(Event ev)
        {
            return "hola";
            //string jsonString = JsonSerializer.Serialize(ev.);
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
            return "{\"" +/*nombre+*/"\":[";
        }
        public string finSerializacion()
        {
            return "]}";
        }
        public Serialiador.Formatos getTipo()
        {
            return Serialiador.Formatos.JSON;
        }
        public string getExtension()
        {
            return ".json";
        }
    }
}
