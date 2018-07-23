namespace Api.Options
{
    public class DatabaseOptions
    {
        public string CompanionTownConnectionString { get; set; }

        public string UsersCollection { get; set; }

        public string AnimalsCollection { get; set; }

        public string HangfireConnectionString { get; set; }
    }
}