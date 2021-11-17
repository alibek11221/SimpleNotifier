using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Notifier.Services;
using Quartz;

namespace Notifier.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IConfiguration _configuration;

        public NotificationJob(ISubscriptionService subscriptionService, IConfiguration configuration)
        {
            _subscriptionService = subscriptionService;
            _configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var subscriptions = await _subscriptionService.GetSubscriptionToNotify(DateTime.Today);
            var credentials = new NetworkCredential();
            var gmailClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = credentials
            };
            _configuration.GetSection("Smtp").Bind(credentials);
            var message = new MailMessage();
            message.From = new MailAddress(credentials.UserName);
            foreach (var subscription in subscriptions.Where(x => x.Clients is not null)){
                foreach (var client in subscription.Clients){
                    message.To.Add(client.Email);
                }
                message.Subject = subscription.Text.Subject;
                message.Body = subscription.Text.Body;
                await gmailClient.SendMailAsync(message);
                message.Dispose();
                await _subscriptionService.SetNextDate(subscription);
            }
            gmailClient.Dispose();
        }
    }
}