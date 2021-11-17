using System;

namespace Notifier.Data.Common
{
    public static class HasTimestampsExtensions
    {
        public static string ToStampString(this IHasTimestamps entity)
        {
            return
                $"{GetStamp("Added", entity.Created)}{GetStamp("Modified", entity.Modified)}{GetStamp("Deleted", entity.Deleted)}";

            string GetStamp(string state, DateTime? dateTime)
                => dateTime == null ? "" : $" {state} on: {dateTime}";
        }
    }
}