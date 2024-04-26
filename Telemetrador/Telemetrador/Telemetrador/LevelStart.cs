
namespace TelemetradorNamespace
{
    public class LevelStart : Event
    {
        public LevelStart(float timestamp)
        {
            setEventType(EventType.startGame);
            data.Add("name", "LevelStart");
            data.Add("timestamp", timestamp.ToString());
        }
    }
}
