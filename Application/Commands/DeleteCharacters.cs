using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteCharacters
    {
        public class Command : IRequest<List<Character>>
        {
            public required List<Character> Characters { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Character>>
        {
            public async Task<List<Character>> Handle(Command request, CancellationToken cancellationToken)
            {
                var characterIds = request.Characters.Select(u => u.Id).ToList();
                var charactersToDelete = await context.Characters
                    .Where(u => characterIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.Characters.RemoveRange(charactersToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return charactersToDelete;
            }
        }
    }
}
