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
        string serializa();
        string getExtension();
        Formatos getTipo();
    }
}
