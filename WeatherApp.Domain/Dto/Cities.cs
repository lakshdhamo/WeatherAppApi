namespace WeatherApp.Domain.Dto
{
    public record Cities
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public decimal Latitude { get; init; }

        public decimal Longitude { get; init; }

        public string Country { get; init; }



    }
}
