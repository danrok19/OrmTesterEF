using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddEquipmentsToCharacters
    {
        public class Command : IRequest<List<Character>>
        {
            public required List<Character> CharactersWithEquipments { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Character>>
        {
            public async Task<List<Character>> Handle(Command request, CancellationToken cancellationToken)
            {
                var characterIds = request.CharactersWithEquipments.Select(c => c.Id).ToList();

                var existingCharacters = await context.Characters
                    .Where(c => characterIds.Contains(c.Id))
                    .Include(c => c.Equipments)
                    .ToListAsync(cancellationToken);

                foreach (var character in request.CharactersWithEquipments)
                {
                    var existingCharacter = existingCharacters.FirstOrDefault(c => c.Id == character.Id);

                    foreach (var equipment in character.Equipments)
                    {
                        if (!existingCharacter.Equipments.Any(e => e.Id == equipment.Id))
                        {
                            existingCharacter.Equipments.Add(equipment);
                        }
                    }
                    
                }

                await context.SaveChangesAsync(cancellationToken);
                return request.CharactersWithEquipments;
            }
        }
    }
}
