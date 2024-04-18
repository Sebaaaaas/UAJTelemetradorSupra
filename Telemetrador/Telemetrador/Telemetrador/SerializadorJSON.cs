using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelemetradorNamespace
{
    internal class SerializadorJSON:Serialiador
    {
        public string serializa()
        {
            return "hola";
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
