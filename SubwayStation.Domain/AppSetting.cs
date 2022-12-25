namespace SubwayStation.Domain
{
    public class AppSetting
    {
        public string JwtSecret { get; set; }
        public int JwtExpiration { get; set; }
        public int JwtRefreshExpiration { get; set; }
    }
}
