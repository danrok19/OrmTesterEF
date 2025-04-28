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
    public class GetCharactersRandom
    {
        public class Query : IRequest<List<Character>>
        {
            public required int count {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Character>>
        {
            public async Task<List<Character>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Characters
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
