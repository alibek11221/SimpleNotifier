using System;

namespace Notifier.Data.Common
{
    public interface IHasTimestamps
    {
        DateTime? Created { get; set; }
        DateTime? Deleted { get; set; }
        DateTime? Modified { get; set; }
    }
}