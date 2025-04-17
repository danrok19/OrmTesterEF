using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetUserList
    {
        public class Query : IRequest<List<User>> { }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<User>>
        {
            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Users.ToListAsync(cancellationToken);
            }
        }
    }
}
