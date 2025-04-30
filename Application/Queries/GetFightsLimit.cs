using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetFightsLimit
    {
        public class Query : IRequest<List<Fight>>
        {
            public required int count { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Fight>>
        {
            public async Task<List<Fight>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Fights
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
