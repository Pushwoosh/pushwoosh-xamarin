using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using System.Linq;

namespace Pushwoosh.iOS
{
    class InAppManager : IInAppManager
    {
        PWInAppManager nativeManager;
        public InAppManager(PWInAppManager inAppManager)
        {
            nativeManager = inAppManager;
        }

        public void SetUserId(string userId) 
        {
            nativeManager.SetUserId(new NSString(userId));
        }

        public Task<RequestResult> PostEventAsync(string anEvent, Dictionary<string, string> attributes)
        {
            var taskSource = new TaskCompletionSource<RequestResult>();
            NSDictionary attrs = attributes != null ? NSDictionary.FromObjectsAndKeys(attributes.Values.ToArray(), attributes.Keys.ToArray()) : new NSDictionary();
            nativeManager.PostEvent(new NSString(anEvent), attrs, (error) => 
            {
                taskSource.SetResult(new RequestResult { Error = PushManager.ErrorFromNSError(error) });
            });

            return taskSource.Task;
        }

        public Task<RequestResult> MergeUserId(string oldUserId, string newUserId, bool doMerge)
        {
            var taskSource = new TaskCompletionSource<RequestResult>();
            nativeManager.MergeUserId(new NSString(oldUserId), new NSString(newUserId), doMerge, (error) =>
            {
                taskSource.SetResult(new RequestResult { Error = PushManager.ErrorFromNSError(error) });
            });

            return taskSource.Task;
        }
    }
}
