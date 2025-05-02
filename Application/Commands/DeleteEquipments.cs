using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteEquipments
    {
        public class Command : IRequest<List<Equipment>>
        {
            public required List<Equipment> Equipments {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Equipment>>
        {
            public async Task<List<Equipment>> Handle(Command request, CancellationToken cancellationToken)
            {
                var equipmentIds = request.Equipments.Select(u => u.Id).ToList();
                var equipmentsToDelete = await context.Equipments
                    .Where(u => equipmentIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.Equipments.RemoveRange(equipmentsToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return equipmentsToDelete;
            }
        }
    }
}
