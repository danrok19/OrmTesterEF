using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetUsersWoutAny
    {
        public class Query : IRequest<List<User>>
        {
            public required int count { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, List<User>>
        {
            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Users
                    .Where(b => b.AccountDetails == null)
                    .Where(b => !b.Characters.Any())
                    .OrderBy(b => Guid.NewGuid())
                    .Take(request.count)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
