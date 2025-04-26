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
    public class CreateCharacters
    {
        public class Command : IRequest<List<Character>>
        {
            public required List<Character> Characters {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Character>>
        {
            public async Task<List<Character>> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Characters.AddRangeAsync(request.Characters, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return request.Characters;
            }
        }
    }
}
