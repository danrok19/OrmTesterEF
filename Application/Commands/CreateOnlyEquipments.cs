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
    public class CreateOnlyEquipments
    {
        public class Command : IRequest<int>
        {
            public required List<Equipment> Equipments { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Equipments.AddRangeAsync(request.Equipments, cancellationToken);
                var addedCount = await context.SaveChangesAsync(cancellationToken);
                return addedCount; // liczba dodanych rekordów
            }
        }
    }
}
