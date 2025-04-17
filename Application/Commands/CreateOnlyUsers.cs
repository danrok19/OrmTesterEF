using Domain;
using MediatR;
using Persistence;


namespace Application.Commands
{
    public class CreateOnlyUsers
    {
        public class Command : IRequest<int>
        {
            public required List<User> Users { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, int>
        {
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Users.AddRangeAsync(request.Users, cancellationToken);
                var addedCount = await context.SaveChangesAsync(cancellationToken);
                return addedCount; // liczba dodanych rekordów
            }
        }
    }
}