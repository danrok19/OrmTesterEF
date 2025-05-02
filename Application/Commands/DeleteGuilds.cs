using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
namespace Application.Commands
{
    public class DeleteGuilds
    {
        public class Command : IRequest<List<Guild>>
        {
            public required List<Guild> Guilds { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Guild>>
        {
            public async Task<List<Guild>> Handle(Command request, CancellationToken cancellationToken)
            {
                var guildIds = request.Guilds.Select(u => u.Id).ToList();
                var guildsToDelete = await context.Guilds
                    .Where(u => guildIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.Guilds.RemoveRange(guildsToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return guildsToDelete;
            }
        }
    }
}
