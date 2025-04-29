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
    public class UpdateOnlyEquipments
    {
        public class Command : IRequest<List<Equipment>>
        {
            public required List<Equipment> Equipments {  get; set; }

            public required List<Equipment> NewData { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Equipment>>
        {
            public async Task<List<Equipment>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedEquipments = new List<Equipment>();

                for (int i = 0; i < request.Equipments.Count; i++)
                {
                    var equipment = request.Equipments[i];
                    var newData = request.NewData[i];

                    var existingEquipment = await context.Equipments.FindAsync(equipment.Id);
                    if (existingEquipment != null)
                    {
                        existingEquipment.Name = newData.Name;
                        existingEquipment.Type = newData.Type;
                        existingEquipment.Statistics = newData.Statistics;
                        existingEquipment.Cost = newData.Cost;

                        updatedEquipments.Add(existingEquipment);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedEquipments;
            }
        }
    }
}
