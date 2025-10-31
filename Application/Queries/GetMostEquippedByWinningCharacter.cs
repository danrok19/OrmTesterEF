using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetMostEquippedByWinningCharacter
    {
        public class Query : IRequest<Equipment?> 
        {
            public required long bossId { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Equipment?>
        {
            public async Task<Equipment?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Equipments
                    .Where(e => e.Characters
                        .Any(c => c.Fights
                            .Any(f => f.Boss.Id == request.bossId && f.IsCharacterWin)))
                    .Select(e => new 
                    {
                        Equipment = e,
                        Count = e.Characters
                            .SelectMany(c => c.Fights)
                            .Count(f => f.Boss.Id == request.bossId && f.IsCharacterWin)
                    })
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Equipment)
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
