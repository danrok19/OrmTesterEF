using API.DTOs;
using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Diagnostics;

namespace API.Controllers
{
    public class ResultController() : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await Mediator.Send(new GetUserList.Query());
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(User user)
        {
            return await Mediator.Send(new CreateUser.Command {  User = user });
        }



        [HttpPost("{count}")]
        public async Task<ActionResult<CreateDTO>> CreateUsers(int count)
        {
            CreateDTO createDTO = new CreateDTO();
            for (int j = 0; j < 4; j++)
            {
                List<User> users = new List<User>();
                for (int i = 0; i < count; i++)
                {
                    User user = new User
                    {
                        Username = GenerateRandomString(20),
                        Password = GenerateRandomString(15)
                    };

                    users.Add(user);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new CreateOnlyUsers.Command { Users = users });
                stopwatch.Stop();
                createDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }

            var random = new Random();
            for (int j = 0; j < 3; j++)
            {
                List<Boss> bosses = new List<Boss>();
                for (int i = 0; i < count; i++)
                {
                    Boss boss = new Boss
                    {
                        Name = GenerateRandomString(18),
                        DifficultyLevel = random.Next(1, 101)
                    };

                    bosses.Add(boss);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new CreateOnlyBosses.Command { Bosses = bosses });
                stopwatch.Stop();
                createDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }

            return createDTO;
        }

        private string GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}
