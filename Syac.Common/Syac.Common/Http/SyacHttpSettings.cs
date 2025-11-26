using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syac.Common.Http;

public class SyacHttpSettings
{
    public Dictionary<string, HttpServiceConfig> Services { get; set; } = [];
}
