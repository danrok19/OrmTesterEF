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
    public class GetEquipmentsRandom
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
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
