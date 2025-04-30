using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
    public class GetGuildsLimit
    {
        public class Query : IRequest<List<Guild>>
        {
            public required int count {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Guild>>
        {
            public async Task<List<Guild>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Guilds
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
