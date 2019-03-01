
namespace Pushwoosh.Geozones
{
    public abstract class GeozonesManager
    {
        static GeozonesManager manager = null;
        public static GeozonesManager Instance
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
            }
        }

        public abstract void StartLocationTracking();

        public abstract void StopLocationTracking();
    }
}
