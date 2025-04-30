using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetEquipmentsUsed
    {

        public class Query : IRequest<List<Equipment>>
        {
            public required int count { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Equipment>>
        {
            public async Task<List<Equipment>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Equipments
                    .Where(e => e.Characters.Any())
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}

