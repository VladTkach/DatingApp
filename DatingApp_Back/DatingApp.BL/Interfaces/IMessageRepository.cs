using DatingApp.Common.DTO.Message;
using DatingApp.Common.DTO.Paged;
using DatingApp.DAL.Entities;

namespace DatingApp.BL.Interfaces;

public interface IMessageRepository
{
    void AddMessage(Message message);
    void DeleteMessage(Message message);
    Task<Message> GetMessage(int id);

    Task<PagesList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
    Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);

    Task<bool> SaveAllAsync();
}