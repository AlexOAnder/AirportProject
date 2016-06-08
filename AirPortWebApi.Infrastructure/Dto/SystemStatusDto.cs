namespace AirPortWebApi.Infrastructure.Dto
{
    public class SystemStatusDto
    {
        public SectionDto TechPark { get; set; } // id 1
        public SectionDto WeatherCenter { get; set; } // 2
        public SectionDto SecurityCenter { get; set; } // 3
        public SectionDto RepairCenter { get; set; } // 4
        public SectionDto AirRunways { get; set; } // 5
    }
}
