using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
    public class GetEquipmentWoutCharacter
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
                    .Where(e => !e.Characters.Any())
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
