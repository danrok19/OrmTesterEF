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
    public class UpdateAccountDetails
    {
        public class Command : IRequest<List<AccountDetails>>
        {
            public required List<AccountDetails> AccountDetails {  get; set; }

            public required List<AccountDetails> NewData { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<AccountDetails>>
        {
            public async Task<List<AccountDetails>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedAccountDetails = new List<AccountDetails>();

                for (int i = 0; i < request.AccountDetails.Count; i++)
                {
                    var accountDetails = request.AccountDetails[i];
                    var newData = request.AccountDetails[i];

                    var existingAccountDetails = await context.AccountDetails.FindAsync(accountDetails.Id);
                    if (existingAccountDetails != null)
                    {
                        existingAccountDetails.Email = newData.Email;
                        existingAccountDetails.IsPremium = newData.IsPremium;
                        //existingAccountDetails.SignupDate = newData.SignupDate;

                        updatedAccountDetails.Add(existingAccountDetails);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedAccountDetails;
            }
        }
    }
}
