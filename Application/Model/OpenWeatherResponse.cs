

namespace Application.DTO
{
    public class OpenWeatherResponse
    {
        public Coordination coord { get; set; } = default!;
        public Main main { get; set; } = default!;
        public Wind wind { get; set; } = default!;
    }

    public class Coordination   
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int humidity { get; set; }
    }
    public class Wind
    {
        public double speed { get; set; }
    }

}
