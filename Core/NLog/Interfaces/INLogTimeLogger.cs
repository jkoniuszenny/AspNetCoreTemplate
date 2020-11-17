using System;
using System.Collections.Generic;
using System.Text;

namespace Core.NLog.Interfaces
{
    public interface INLogTimeLogger
    {
        void LogWarn(string message);
    }
}
