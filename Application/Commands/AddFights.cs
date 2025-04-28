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
    public class AddFights
    {
        public class Command : IRequest<List<Fight>>
        {
            public required List<Fight> Fights { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Fight>>
        {
            public async Task<List<Fight>> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Fights.AddRangeAsync(request.Fights, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return request.Fights;
            }
        }
    }
}
