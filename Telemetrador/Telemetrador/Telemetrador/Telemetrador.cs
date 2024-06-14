using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using TelemetradorNamespace;

namespace TelemetradorNamespace
{
    public class Telemetrador
    {
        
        private static Telemetrador instance = null;
        private int eventIDcounter;
        public Guid idSesion; //para guardar la sesion del usuario
        private Persistencia _persistencia;
        private Serializador _serializador;
        float t=2.0f;


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
            _persistencia.addEvent(ev);
            _persistencia.flush();
            
        }
        public void endSession(float timestamp, bool win,float min, float sec, float mill)
        {
            EndGame ev = new EndGame(timestamp, win, min,  sec, mill);
            ev.setID(eventIDcounter++);
            _persistencia.addEvent(ev);
            //_persistencia.close();
            
        }
        public void endQuit(float timestamp)
        {
            EndQuit ev = new EndQuit(timestamp);
            ev.setID(eventIDcounter++);
            _persistencia.addEvent(ev);
            _persistencia.close();

        }
        public void addEvent(Event ev)
        {
            ev.setID(eventIDcounter++);
            _persistencia.addEvent(ev);
        }
       public void Update(float deltaTime)
        {
            t -= deltaTime;
            if (t <= 0)
            {
                _persistencia.flush();
                t = 2.0f;
            }
        }
    }
}
