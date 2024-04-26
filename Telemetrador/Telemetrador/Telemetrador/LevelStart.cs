
namespace TelemetradorNamespace
{
    public class LevelStart : Event
    {
        public LevelStart(float timestamp)
        {
            setEventType(EventType.startLevel);
            data.Add("name", "Empiece_Nivel");
            data.Add("timestamp", timestamp.ToString());
        }
    }
}
