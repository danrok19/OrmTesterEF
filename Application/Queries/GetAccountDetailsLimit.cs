using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Queries
{
    public class GetAccountDetailsLimit
    {
        public class Query : IRequest<List<AccountDetails>>
        {
            public required int count { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<AccountDetails>>
        {
            public async Task<List<AccountDetails>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.AccountDetails
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }

    }
}
