using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Settings
{
    public class DatabaseSettings : DbSettings
    {
        public override string ConnectionString { get; set; }
    }
}
