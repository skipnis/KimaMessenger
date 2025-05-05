using Contracts.Interfaces.Repositories;
using Core;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContactRepository : Repository<Contact, long>, IContactRepository
{
    private readonly DbSet<Contact> _contacts; 
    
    public ContactRepository(ApplicationDbContext context) : base(context)
    {
        _contacts = context.Set<Contact>();
    }

    public async Task<IEnumerable<User>> GetContactsByUserIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await _contacts
            .Where(c => c.UserId == userId)
            .Select(c => c.ContactUser)
            .ToListAsync(cancellationToken);
    }

    public async Task AddContactAsync(long userId, long contactUserId, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            UserId = userId,
            ContactUserId = contactUserId
        };

        await _contacts.AddAsync(contact, cancellationToken);
    }
    
    public async Task<bool> ContactExistsAsync(long userId, long contactUserId, CancellationToken cancellationToken)
    {
        return await _contacts
            .AnyAsync(c => c.UserId == userId && c.ContactUserId == contactUserId, cancellationToken);
    }
}