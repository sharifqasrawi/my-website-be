using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IEmailMessageRepository
    {
        EmailMessage Create(EmailMessage emailMessage);
        IEnumerable<EmailMessage> GetEmailMessages();
    }
}
