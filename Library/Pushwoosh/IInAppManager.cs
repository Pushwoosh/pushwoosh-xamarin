using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pushwoosh
{
    public interface IInAppManager
    {
        void SetUserId(string userId);
        Task<RequestResult> PostEventAsync(string anEvent, Dictionary<string, string> attributes);
        Task<RequestResult> MergeUserId(string oldUserId, string newUserId, bool doMerge);
    }
}
