using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateOnlyBosses
    {
        public class Command : IRequest<List<Boss>>
        {
            public required List<Boss> Bosses { get; set; }

            public required List<Boss> NewData { get; set; }
        }
        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Boss>>
        {
            public async Task<List<Boss>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedBosses = new List<Boss>();

                for (int i = 0; i < request.Bosses.Count; i++)
                {
                    var boss = request.Bosses[i];
                    var newData = request.NewData[i];

                    var existingBoss = await context.Bosses.FindAsync(boss.Id);
                    if (existingBoss != null)
                    {
                        existingBoss.Name = newData.Name;
                        existingBoss.DifficultyLevel = newData.DifficultyLevel;

                        updatedBosses.Add(existingBoss);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedBosses;
            }
        }
    }
}
