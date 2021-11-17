using System.ComponentModel.DataAnnotations;

namespace Notifier.Data.Enums
{
    public enum SubscriptionPolicy
    {
        [Display(Name = "День(Дней, Дня)")]
        Daily,
        [Display(Name = "Месяц(Месяца, Месяцев)")]
        Monthly,
        [Display(Name = "Год(Года, Лет)")]
        Yearly,
    }
}