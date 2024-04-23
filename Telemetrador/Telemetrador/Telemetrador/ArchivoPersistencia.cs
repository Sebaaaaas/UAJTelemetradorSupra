using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelemetradorNamespace
{
    internal class ArchivoPersistencia:Persistencia
    {
        
        private string nombreArchivo;
        private StreamWriter archivo;
        public ArchivoPersistencia(string nombre,Serializador s):base(s)
        {
            nombreArchivo = nombre + s.getExtension();
            archivo=new StreamWriter(nombreArchivo,true);
            archivo.WriteLine(s.inicioSerializacion());
        }

        public override void flush()
        {
            archivo.Write(serializador.serializaTodo(ref events));
        }
        public override void close()
        {
            archivo.WriteLine(serializador.finSerializacion());
            archivo.Flush();
        }
    }
}
