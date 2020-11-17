using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Settings
{
    public class ResponseTimeSettings
    {
        public int MilisecondsElapsedToNotify { get; set; }
        public bool Enabled { get; set; }
    }
}
