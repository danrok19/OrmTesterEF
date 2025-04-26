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
    public class GetCharactersWOutGuild
    {
        public class Query : IRequest<List<Character>> {}

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<Character>>
        {
            public async Task<List<Character>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Characters
                    .Where(c => c.GuildId == null)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
