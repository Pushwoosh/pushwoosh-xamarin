﻿using System;
using ObjCRuntime;

namespace Pushwoosh.Inbox
{
    [Native]
    public enum PWInboxMessageType : long
    {
        Plain = 0,
        Richmedia = 1,
        Url = 2,
        Deeplink = 3
    }
}
