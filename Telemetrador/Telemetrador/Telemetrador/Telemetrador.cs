using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TelemetradorNamespace
{
    public class Telemetrador
    {
        
        private static Telemetrador instance = null;
        private int eventIDcounter;
        public Guid idSesion; //para guardar la sesion del usuario
        private Persistencia _persistencia;
        private Serializador _serializador;


        private Telemetrador()
        {
            idSesion = Guid.NewGuid();
            eventIDcounter = 0;
        }
        public static Telemetrador Instance()
        {
            return instance;
        }

        public static bool Init(Formatos formato,string nombre,Medio medio)
        {
            if (instance != null) return false;

            instance = new Telemetrador();
            instance.initSerializador(formato);
            instance.initPersistencia(medio,nombre);
            
            return true;
        }
        private void initSerializador(Formatos formato)
        {
            switch (formato)
            {
                case Formatos.JSON:
                    _serializador=new SerializadorJSON();
                    break;
            }
        }
        private void initPersistencia(Medio medio,string nombre)
        {
            switch (medio)
            {
                case Medio.Archivo:
                    _persistencia=new ArchivoPersistencia(nombre,_serializador);
                    break;
            }
        }

        public void startSession(float timestamp, string nombreJuego_)
        {
            StartGame ev = new StartGame(timestamp, nombreJuego_);
            ev.setID(eventIDcounter++);
            //events.Append(ev);
        }
        public void endSession(float timestamp, bool win)
        {
            EndGame ev = new EndGame(timestamp, win);
            ev.setID(eventIDcounter++);
            //events.Append(ev);
        }
        //public void addEvent(Event ev)
        //{
        //    ev.setID(eventIDcounter++);
        //    events.Append(ev);
        //}
    }
}
