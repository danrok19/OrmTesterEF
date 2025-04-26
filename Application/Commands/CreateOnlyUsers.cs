using Domain;
using MediatR;
using Persistence;


namespace Application.Commands
{
    public class CreateOnlyUsers
    {
        public class Command : IRequest<List<User>>
        {
            public required List<User> Users { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<User>>
        {
            public async Task<List<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                await context.Users.AddRangeAsync(request.Users, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return request.Users; // liczba dodanych rekordów
            }
        }
    }
}