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
    public class GetRandomBossId
    {
        public class Query : IRequest<long>
        {
        }
        public class Handler(AppDbContext context) : IRequestHandler<Query, long>
        {
            public async Task<long> Handle(Query request, CancellationToken cancellationToken)
            {
                var bossIds = await context.Bosses
                    .Select(b => b.Id)
                    .ToListAsync(cancellationToken);
                if (bossIds.Count == 0)
                {
                    throw new InvalidOperationException("No bosses found in the database.");
                }
                var random = new Random();
                int randomIndex = random.Next(bossIds.Count);
                return bossIds[randomIndex];
            }
        }
    }
}
