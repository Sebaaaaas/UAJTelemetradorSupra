using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    internal interface Serialiador
    {
        enum Formatos {JSON};
        string serializa(Event ev);
        string serializaTodo(ref Queue<Event> events);
        string getExtension();
        Formatos getTipo();
    }
}
