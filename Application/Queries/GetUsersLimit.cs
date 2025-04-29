using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetUsersLimit
    {
        public class Query : IRequest<List<User>> 
        { 
            public required int count {  get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<User>>
        {
            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Users
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
