
namespace TelemetradorNamespace
{
    public class LevelStart : Event
    {
        public LevelStart(float timestamp)
        {
            setEventType(EventType.startGame);
            data.Add("name", "Empiece_Nivel");
            data.Add("timestamp", timestamp.ToString());
        }
    }
}
