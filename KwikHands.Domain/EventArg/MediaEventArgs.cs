﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KwikHands.Domain.EventArg
{
    public class MediaEventArgs : EventArgs
    {
        public Uri MediaFile { get; set; }
    }
}