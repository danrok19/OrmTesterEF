using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands
{
    public class DeleteAccountDetails
    {
        public class Command : IRequest<List<AccountDetails>>
        {
            public required List<AccountDetails> AccountDetails { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<AccountDetails>>
        {
            public async Task<List<AccountDetails>> Handle(Command request, CancellationToken cancellationToken)
            {
                var accountDetailsIds = request.AccountDetails.Select(u => u.Id).ToList();
                var accountDetailsToDelete = await context.AccountDetails
                    .Where(u => accountDetailsIds.Contains(u.Id))
                    .ToListAsync(cancellationToken);

                context.AccountDetails.RemoveRange(accountDetailsToDelete);
                await context.SaveChangesAsync(cancellationToken);

                return accountDetailsToDelete;
            }
        }
    }
}
