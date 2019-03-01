
namespace Pushwoosh.Geozones.Droid
{
    public class GeozonesManager : Pushwoosh.Geozones.GeozonesManager
    {
        public static new GeozonesManager Instance => global::Pushwoosh.Geozones.GeozonesManager.Instance as GeozonesManager;

        public static void Init()
        {
            global::Pushwoosh.Geozones.GeozonesManager.Instance = new GeozonesManager();
        }

        public override void StartLocationTracking()
        {
            Location.PushwooshLocation.StartLocationTracking();
        }

        public override void StopLocationTracking()
        {
            Location.PushwooshLocation.StopLocationTracking();
        }
    }
}
