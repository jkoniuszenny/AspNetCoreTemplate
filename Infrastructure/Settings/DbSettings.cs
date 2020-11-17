using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Settings
{
    public abstract class DbSettings
    {
        public abstract string ConnectionString { get; set; }
    }
}
