using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TelemetradorNamespace
{
    internal class ArchivoPersistencia:Persistencia
    {
        
        private string nombreArchivo;
        private StreamWriter archivo;
        public ArchivoPersistencia(string nombre,Serializador s):base(s)
        {
            nombreArchivo = nombre + s.getExtension();
            //File.WriteAllText(nombreArchivo, serializador.inicioSerializacion());
            archivo = new StreamWriter(nombreArchivo, true);
            archivo.WriteLine(s.inicioSerializacion());
        }

        public override void flush()
        {
            archivo.Write(serializador.serializaTodo(ref events));
            //string contenido = serializador.serializaTodo(ref events);
            //File.WriteAllText(nombreArchivo, serializador.serializaTodo(ref events));
        }
        public override void close()
        {
            //archivo.Flush();
            flush();
            archivo.WriteLine(serializador.finSerializacion());
            archivo.Close();
           // File.AppendAllText(nombreArchivo, serializador.finSerializacion());
        }
    }
}
