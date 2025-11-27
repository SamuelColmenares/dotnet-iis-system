using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Common.Cache;

public class SyacCacheSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string InstanceName { get; set; } = "Syac_";
    public int? DefaultExpirationInMinutes { get; set; } = 30;
}
