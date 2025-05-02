using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteOnlyUsers
    {
        public class Command : IRequest<List<User>>
        {
            public required List<User> Users { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<User>>
        {
            public async Task<List<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userIds = request.Users.Select(u => u.Id).ToList();
                var usersToDelete = await context.Users
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.Users.RemoveRange(usersToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return usersToDelete;
            }
        }
    }
}
