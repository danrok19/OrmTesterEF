using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteFights
    {
        public class Command : IRequest<List<Fight>>
        {
            public required List<Fight> Fights { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Fight>>
        {
            public async Task<List<Fight>> Handle(Command request, CancellationToken cancellationToken)
            {
                var deletedFights = new List<Fight>();

                foreach (var fight in request.Fights)
                {
                    var existingFight = await context.Fights.FirstOrDefaultAsync(
                        f => f.CharacterId == fight.CharacterId &&
                             f.BossId == fight.BossId &&
                             f.Time == fight.Time,
                        cancellationToken);

                    if (existingFight != null)
                    {
                        context.Fights.Remove(existingFight);
                        deletedFights.Add(existingFight);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return deletedFights;
            }
        }
    }
}
