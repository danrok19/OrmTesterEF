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
    public class CreateAccountDetails
    {
        public class Command : IRequest<List<AccountDetails>>
        {
            public required List<AccountDetails> AccountDetails { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<AccountDetails>>
        {
            public async Task<List<AccountDetails>> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.AccountDetails.AddRangeAsync(request.AccountDetails, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return request.AccountDetails;
            }
        }
    }
}
