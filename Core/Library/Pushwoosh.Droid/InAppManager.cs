using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pushwoosh.InApp;
using Pushwoosh.Function;

namespace Pushwoosh.Droid
{
    public class InAppManager : IInAppManager
    {
        PushwooshInApp nativeManager;
        public InAppManager(PushwooshInApp nativeManager) {
            this.nativeManager = nativeManager;
        }

        public Task<RequestResult> MergeUserId(string oldUserId, string newUserId, bool doMerge)
        {
            var taskSource = new TaskCompletionSource<RequestResult>();
            nativeManager.MergeUserId(oldUserId, newUserId, doMerge, new PushManager.Callback() { ResultCallback = (Result result) => {
                    taskSource.SetResult(new RequestResult() { Error = PushManager.ErrorFromJavaException(result.Exception) });
                } });
            return taskSource.Task;
        }

        public Task<RequestResult> PostEventAsync(string anEvent, Dictionary<string, string> attributes)
        {
            var taskSource = new TaskCompletionSource<RequestResult>();
            var tagsBundleBuilder = new Tags.TagsBundle.Builder();
            if (attributes != null)
            {
                foreach (var kv in attributes)
                {
                    tagsBundleBuilder.PutString(kv.Key, kv.Value);
                }
            }
            nativeManager.PostEvent(anEvent, tagsBundleBuilder.Build(), new PushManager.Callback() { ResultCallback = (Result result) => {
                    taskSource.SetResult(new RequestResult() { Error = PushManager.ErrorFromJavaException(result.Exception) });
                } });

            return taskSource.Task;
        }

        public void SetUserId(string userId)
        {
            nativeManager.UserId = userId;
        }
    }
}
