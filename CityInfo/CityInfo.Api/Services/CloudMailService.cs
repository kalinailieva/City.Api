namespace CityInfo.Api.Services
{
    public class CloudMailService: IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailTo"];
            _mailFrom = configuration["mailSettings:mailFrom"];
        }

        public void Send(string subject, string message) => Console.WriteLine($"{subject}-{message} - was send from cloud mail service");
    }
}
