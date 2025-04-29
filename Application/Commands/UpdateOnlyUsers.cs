using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateOnlyUsers
    {

        public class Command : IRequest <List<User>>
        {
            public required List<User> Users { get; set; }

            public required List<User> NewData { get; set; }
        }
        public class Handler(AppDbContext context) : IRequestHandler<Command, List<User>>
        {
            public async Task<List<User>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedUsers = new List<User>();

                for (int i = 0; i < request.Users.Count; i++)
                {
                    var user = request.Users[i];
                    var newData = request.NewData[i];

                    var existingUser = await context.Users.FindAsync(user.Id);
                    if (existingUser != null)
                    {
                        existingUser.Username = newData.Username;
                        existingUser.Password = newData.Password;

                        updatedUsers.Add(existingUser);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedUsers;
            }
        }

    }
}
