using System;
using Pushwoosh.Function;

namespace PushwooshSample
{
    class CallbackWrapper : Java.Lang.Object, ICallback
    {
        public Action<Result> Callback { get; set; }
        public void Process(Result result)
        {
            Callback(result);
        }
    }
}
