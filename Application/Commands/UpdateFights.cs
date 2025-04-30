using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands
{
    public class UpdateFights
    {
        public class Command : IRequest<List<Fight>>
        {
            public required List<Fight> Fights { get; set; }

            public required List<Fight> NewData { get; set; }

        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Fight>>
        {
            public async Task<List<Fight>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedFights = new List<Fight>();

                for (int i = 0; i < request.Fights.Count; i++)
                {
                    var fight = request.Fights[i];
                    var newData = request.NewData[i];

                    var existingFight = await context.Fights
                        .FirstOrDefaultAsync(f => f.CharacterId == fight.CharacterId && f.BossId == fight.BossId, cancellationToken);
                    if (existingFight != null)
                    {
                        existingFight.IsCharacterWin = newData.IsCharacterWin;
                        existingFight.Time = newData.Time;

                        updatedFights.Add(existingFight);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedFights;
            }
        }
    }
}
