using MediatR;
using Domain;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetBossesLimit
    {
        public class Query : IRequest<List<Boss>>
        {
            public required int limit {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Boss>>
        {
            public async Task<List<Boss>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Bosses
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.limit)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
