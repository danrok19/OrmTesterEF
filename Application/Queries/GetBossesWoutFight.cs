using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetBossesWoutFight
    {
        public class Query : IRequest<List<Boss>>
        {
            public required int count { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Boss>>
        {
            public async Task<List<Boss>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Bosses
                    .Where(b => !b.Fights.Any())
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
