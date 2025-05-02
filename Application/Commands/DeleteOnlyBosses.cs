using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;


namespace Application.Commands
{
    public class DeleteOnlyBosses
    {
        public class Command : IRequest<List<Boss>>
        {
            public required List<Boss> Bosses { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Boss>>
        {
            public async Task<List<Boss>> Handle(Command request, CancellationToken cancellationToken)
            {
                var bossIds = request.Bosses.Select(u => u.Id).ToList();
                var bossesToDelete = await context.Bosses
                    .Where(u => bossIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.Bosses.RemoveRange(bossesToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return bossesToDelete;
            }
        }
    }
}
