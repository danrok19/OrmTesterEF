using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands
{
    public class UpdateCharacters
    {
        public class Command : IRequest<List<Character>>
        {
            public required List<Character> Characters { get; set; }
            public required List<Character> NewData { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Character>>
        {
            public async Task<List<Character>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedCharacters = new List<Character>();

                for (int i = 0; i < request.Characters.Count; i++)
                {
                    var character = request.Characters[i];
                    var newData = request.NewData[i];

                    var existingCharacter = await context.Characters.FindAsync(character.Id);
                    if (existingCharacter != null)
                    {
                        existingCharacter.CharacterType = newData.CharacterType;
                        existingCharacter.Currency = newData.Currency;

                        updatedCharacters.Add(existingCharacter);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedCharacters;
            }
        }
    }
}
