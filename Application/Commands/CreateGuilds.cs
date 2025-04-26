using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateGuilds
    {
        public class Command : IRequest<List<Guild>>
        {
            public required List<Guild> Guilds { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Guild>>
        {
            public async Task<List<Guild>> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Guilds.AddRangeAsync(request.Guilds, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return request.Guilds;
            }
        }
    }
}
