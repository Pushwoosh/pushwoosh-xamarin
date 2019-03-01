using CoreLocation;
using Foundation;

namespace Pushwoosh.Geozones.iOS.Bindings
{

    // @interface PWGeozonesManager : NSObject
    [BaseType(typeof(NSObject))]
    public interface PWGeozonesManager
    {
        // + (instancetype)sharedManager;
        [Static]
        [Export("sharedManager")]
        PWGeozonesManager SharedManager { get; }

        // -(void)sendLocation:(CLLocation *)location;
        [Export("sendLocation:")]
        void SendLocation(CLLocation location);

        // -(void)startLocationTracking;
        [Export("startLocationTracking")]
        void StartLocationTracking();

        // -(void)stopLocationTracking;
        [Export("stopLocationTracking")]
        void StopLocationTracking();

    }
}
