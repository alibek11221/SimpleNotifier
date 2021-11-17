using System.ComponentModel.DataAnnotations;

namespace Notifier.Data.Enums
{
    public enum SubscriptionType
    {
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Telegram")]
        Telegram,
        [Display(Name = "Sms")]
        Sms
    }
}