using MassTransit;
using NewBookingApp.Core.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NewBookingApp.Email.SendEmail
{
    public class SendEmailConsumer : IConsumer<SendEmailRequestDto>
    {
        private readonly IRequestClient<RequestUserByPassportNumber> _client;
        private readonly IConfiguration _config;

        public SendEmailConsumer(IRequestClient<RequestUserByPassportNumber> client, IConfiguration configuration)
        {
            _client = client;
            _config = configuration;
        }
        public async Task Consume(ConsumeContext<SendEmailRequestDto> context)
        {
           // var api = "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";
            var apiKey = _config.GetSection("apiKey").Value;
            var client = new SendGridClient(apiKey);

            var identityResponse = await _client.GetResponse<GetUserResponse>( new RequestUserByPassportNumber(context.Message.PassengerPassport));

            var identityMessage = identityResponse.Message;

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("carolina.louzada@hotmail.com", "CarolSender"),
                Subject = "Reservation",
                PlainTextContent = $"Hi {context.Message.PassengerName}! Your reservation was created!"
            };

            msg.AddTo(new EmailAddress(identityMessage.PassengerEmail));

            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
        }
    }
}
