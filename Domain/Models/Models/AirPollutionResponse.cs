using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Models
{
    public class AirPollutionResponse
    {
        public List<AirPollutionItem>? list { get; set; }
    }

    public class AirPollutionItem
    {
        public AirMain main { get; set; } = default!;
        public AirComponents components { get; set; } = default!;
    }

    public class AirMain { public int aqi { get; set; } }

    public class AirComponents
    {
        public double co { get; set; }
        public double no { get; set; }
        public double no2 { get; set; }
        public double o3 { get; set; }
        public double so2 { get; set; }
        public double pm2_5 { get; set; }
        public double pm10 { get; set; }
        public double nh3 { get; set; }
    }
}
