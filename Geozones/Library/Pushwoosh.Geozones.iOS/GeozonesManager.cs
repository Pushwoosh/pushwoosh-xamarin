using Pushwoosh.Geozones.iOS.Bindings;

namespace Pushwoosh.Geozones.iOS
{
    public class GeozonesManager : Pushwoosh.Geozones.GeozonesManager
    {
        public static new GeozonesManager Instance => global::Pushwoosh.Geozones.GeozonesManager.Instance as GeozonesManager;

        PWGeozonesManager nativeManager;

        public static void Init()
        {
            global::Pushwoosh.Geozones.GeozonesManager.Instance = new GeozonesManager();
        }

        public GeozonesManager()
        {
            nativeManager = PWGeozonesManager.SharedManager;
        }

        public override void StartLocationTracking()
        {
            nativeManager.StartLocationTracking();
        }

        public override void StopLocationTracking()
        {
            nativeManager.StopLocationTracking();
        }
    }
}
