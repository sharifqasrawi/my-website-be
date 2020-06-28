using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessages();
        Message GetMessage(long id);
        Message Delete(long id);
        Message Send(Message message);
        Message Update(Message messageChanges);
    }
}
