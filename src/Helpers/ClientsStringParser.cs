using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Notifier.Data.Entities;
using Notifier.Dtos.Client;
using Quartz.Util;

namespace Notifier.Helpers
{
    public static class ClientsStringParser
    {
        public static List<Client> ParseClients(this CreateManyClientsDto dto)
        {
            if (dto.ClientsWithEmailsString.IsNullOrWhiteSpace()) return null;
            var clients = dto.ClientsWithEmailsString.Split("\r\n");
            return clients.Where(IsValidEmail)
                .Select(x => new Client{Email = x})
                .ToList();
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            
            try{
                email = Regex.Replace(email,
                @"(@)(.+)$",
                DomainMapper,
                RegexOptions.None,
                TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e){ return false; }
            catch (ArgumentException e){ return false; }

            try{
                return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase,
                TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException){ return false; }
        }
    }
}