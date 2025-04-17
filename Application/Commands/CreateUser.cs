using Domain;
using MediatR;
using Persistence;


namespace Application.Commands
{
    public class CreateUser
    {
        public class Command : IRequest<string>
        {
            public required User User { get; set; }

        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, string>
        {
            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                context.Users.Add(request.User);

                var response = await context.SaveChangesAsync(cancellationToken);

                //Console.WriteLine(response);

                return request.User.Id.ToString();
            }


        }
    }
}
