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
    public class CreateOnlyBosses
    {
        public class Command : IRequest<int>
        {
            public required List<Boss> Bosses { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Bosses.AddRangeAsync(request.Bosses, cancellationToken);
                var addedCount = await context.SaveChangesAsync(cancellationToken);
                return addedCount; // liczba dodanych rekordów
            }
        }
    }
}
