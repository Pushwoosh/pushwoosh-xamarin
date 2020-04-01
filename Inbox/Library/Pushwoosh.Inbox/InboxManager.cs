using System;
using System.Collections.Generic;

namespace Pushwoosh.Inbox
{
    public abstract class InboxManager
    {
        public static InboxManager Instance { get; set; }

        public abstract void PresentInboxUI(PushwooshInboxStyle style);

        public abstract void UnreadMessagesCountWithCompletion(Action<int, string> completion);

        public abstract void AddObserverForUnreadMessagesCount(Action<int> completion);
        
    }
}
