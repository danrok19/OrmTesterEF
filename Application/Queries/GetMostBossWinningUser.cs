using Domain;
using MediatR;
using Persistence;
using System;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries
{
    public class GetMostBossWinningUser
    {
        public class Query : IRequest<User?>
        {
            public required long bossId { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, User?>
        {
            public async Task<User?> Handle(Query request, CancellationToken cancellationToken)
            {
                return await context.Users
                    .Select(u => new
                    {
                        User = u,
                        WinCount = u.Characters
                        .SelectMany(c => c.Fights)
                        .Count(f => f.Boss.Id == request.bossId && f.IsCharacterWin)
                    })
                    .OrderByDescending(x => x.WinCount)
                    .Select(x => x.User)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
