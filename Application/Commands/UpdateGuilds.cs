using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Commands
{
    public class UpdateGuilds
    {
        public class Command : IRequest<List<Guild>>
        {
            public required List<Guild> Guilds { get; set; }

            public required List<Guild> NewData { get; set; }

        }

        public class Handler(AppDbContext context) : IRequestHandler<Command, List<Guild>>
        {
            public async Task<List<Guild>> Handle(Command request, CancellationToken cancellationToken)
            {
                var updatedGuilds = new List<Guild>();

                for (int i = 0; i < request.Guilds.Count; i++)
                {
                    var guild = request.Guilds[i];
                    var newData = request.NewData[i];

                    var existingGuild = await context.Guilds.FindAsync(guild.Id);
                    if (existingGuild != null)
                    {
                        existingGuild.Name = newData.Name;
                        existingGuild.LevelProgression = newData.LevelProgression;

                        updatedGuilds.Add(existingGuild);
                    }
                }

                await context.SaveChangesAsync(cancellationToken);
                return updatedGuilds;
            }
        }
    }
}
