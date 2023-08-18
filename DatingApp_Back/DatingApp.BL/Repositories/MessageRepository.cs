using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.BL.Interfaces;
using DatingApp.Common.DTO.Message;
using DatingApp.Common.DTO.Paged;
using DatingApp.DAL.Context;
using DatingApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.BL.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly DatingAppContext _context;
    private readonly IMapper _mapper;

    public MessageRepository(DatingAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void AddMessage(Message message)
    {
        _context.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        _context.Messages.Remove(message);
    }

    public async Task<Message> GetMessage(int id)
    {
        return await _context.Messages.FindAsync(id);
    }

    public async Task<PagesList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = _context.Messages
            .OrderBy(x => x.MessageSent)
            .AsQueryable();
        query = messageParams.Container switch
        {
            "Inbox" => query.Where(u => u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false),
            "Outbox" => query.Where(u => u.SenderUsername == messageParams.Username && u.SenderDeleted == false),
            _ => query.Where(u =>
                u.RecipientUsername == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
        };

        var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

        return await PagesList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName)
    {
        var messages = await _context.Messages
            .Include(u => u.Sender).ThenInclude(p => p.Photos)
            .Where(m => m.RecipientUsername == currentUserName && m.RecipientDeleted == false &&
                        m.SenderUsername == recipientUserName
                        || m.SenderUsername == currentUserName && m.SenderDeleted == false &&
                        m.RecipientUsername == recipientUserName)
            .OrderBy(m => m.MessageSent)
            .ToListAsync();

        var unreadMessages = messages
            .Where(m => m.DateRead == null && m.RecipientUsername == currentUserName)
            .ToList();

        if (unreadMessages.Any())
        {
            foreach (var message in unreadMessages)
            {
                message.DateRead = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        return _mapper.Map<IEnumerable<MessageDto>>(messages);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}