using API.DTOs;
using Application.Commands;
using Application.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

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

            for (int j = 0; j < 3; j++)
            {
                List<Equipment> equipments = new List<Equipment>();
                for (int i = 0; i < count; i++)
                {
                    Equipment equipment = new Equipment
                    {
                        Name = GenerateRandomString(18),
                        Type = GenerateRandomString(10),
                        Statistics = random.Next(1, 1000),
                        Cost = random.Next(1, 10000),
                    };
                    equipments.Add(equipment);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new CreateOnlyEquipments.Command { Equipments = equipments});
                stopwatch.Stop();
                createDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }

            for (int j = 0; j < 10; j++)
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
                List<User> UsersList = await Mediator.Send(new CreateOnlyUsers.Command { Users = users });

                List<AccountDetails> accountDetails = new List<AccountDetails>();

                for (int i = 0; i < count; i++)
                {
                    AccountDetails details = new AccountDetails
                    {
                        Email = GenerateRandomString(20) + "@mail.com",
                        IsPremium = false,
                        //SignupDate = DateTime.Today,
                        User = users[i]
                    };
                    accountDetails.Add(details);

                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new CreateAccountDetails.Command { AccountDetails = accountDetails });
                stopwatch.Stop();
                createDTO.AddOneToOne(stopwatch.ElapsedMilliseconds);

            }

            for (int j = 0; j < 5; j++)
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
                List<User> UsersList = await Mediator.Send(new CreateOnlyUsers.Command { Users = users });

                List<Character> characters = new List<Character>();

                for (int i = 0; i < count * 2; i++)
                {
                    Character character = new Character
                    {
                        CharacterType = "CharcterType" + i,
                        Currency = random.Next(1, 1000),
                        //LevelProgression = 1,
                        User = users[random.Next(0, users.Count() - 1)]
                    };
                    characters.Add(character);

                }

                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new CreateCharacters.Command { Characters = characters });
                stopwatch.Stop();
                createDTO.AddOneToMany(stopwatch.ElapsedMilliseconds);

                List<Character> charactersList = await Mediator.Send(new GetCharactersWOutGuild.Query { });

                List <Guild> guilds = new List<Guild>();
                for (int i = 0; i < count; i++)
                {
                    Guild guild = new Guild
                    {
                        Name = GenerateRandomString(12),
                        LevelProgression = random.Next(1, 10)
                    };
                    Character c = charactersList[random.Next(0, (charactersList.Count - 1))];
                    guild.add(c);
                    charactersList.Remove(c);

                    Character c1 = charactersList[random.Next(0, (charactersList.Count - 1))];
                    guild.add(c1);
                    charactersList.Remove(c1);

                    guilds.Add(guild);

                }
                Stopwatch stopwatch1 = Stopwatch.StartNew();
                List<Guild> guildList = await Mediator.Send(new CreateGuilds.Command { Guilds = guilds });
                stopwatch1.Stop();
                createDTO.AddOneToMany(stopwatch.ElapsedMilliseconds);

            }

            for (int j = 0; j < 5; j++)
            {
                List<Equipment> equipments = await Mediator.Send(new GetEquipmentsRandom.Query { count = count});
                List<Character> characters = await Mediator.Send(new GetCharactersRandom.Query { count = count});

                List<Character> charactersWithEquipments = new List<Character>();
                Character character = null;
                for (int i = 0; i < count; i++)
                {
                    int addEqCount = random.Next(0, count);
                    character = characters[random.Next(0, characters.Count - 1)];
                    for (int z = 0; z < addEqCount; z++)
                    {
                        character.add(equipments[random.Next(0, equipments.Count - 1)]);
                    }
                    charactersWithEquipments.Add(character);
                    characters.Remove(character);
                    i += addEqCount;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new AddEquipmentsToCharacters.Command { CharactersWithEquipments = charactersWithEquipments });
                stopwatch.Stop();
                createDTO.AddManyToMany(stopwatch.ElapsedMilliseconds);
            }

            for ( int j = 0; j < 5; j++ )
            {
                List<Character> characters = await Mediator.Send(new GetCharactersRandom.Query { count = count });
                List<Boss> bosses = await Mediator.Send(new GetBossesLimit.Query { limit = count });

                List<Fight> fights = new List<Fight>();

                for (int i = 0; i < count; i++)
                {
                    long characterId = characters[random.Next(0, characters.Count - 1)].Id;
                    long bossId = bosses[random.Next(0, bosses.Count - 1)].Id;

                    // Check if a fight with the same CharacterId and BossId already exists in the fights list
                    if (!fights.Any(f => f.CharacterId == characterId && f.BossId == bossId))
                    {
                        Fight fight = new Fight
                        {
                            CharacterId = characterId,
                            BossId = bossId,
                            IsCharacterWin = new Random().Next(0, 2) == 1,
                            Time = DateTime.UtcNow
                        };
                        fights.Add(fight);
                    }
                    else
                    {
                        i--;
                    }

                }
                Stopwatch stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new AddFights.Command { Fights = fights });
                stopwatch.Stop();
                createDTO.AddManyToMany(stopwatch.ElapsedMilliseconds);
            }

            return createDTO;
        }


        [HttpPut("update/{count}")]
        public async Task<ActionResult<UpdateDTO>> UpdateUsers(int count)
        {
            UpdateDTO updateDTO = new UpdateDTO();

            for (int j = 0; j < 4; j++)
            {
                List<User> users = await Mediator.Send(new GetUsersWoutAny.Query { count = count });

                List<User> newUsers = new List<User>();

                for (int i = 0; i < count; i++)
                {
                    User user = new User
                    {
                        Username = GenerateRandomString(20),
                        Password = GenerateRandomString(15)
                    };

                    newUsers.Add(user);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new UpdateOnlyUsers.Command { Users = users, NewData = newUsers });
                stopwatch.Stop();
                updateDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }


            var random = new Random();
            for (int j = 0; j < 3; j++)
            {
                List<Boss> bosses = await Mediator.Send(new GetBossesLimit.Query { limit = count });
                List<Boss> newBosses = new List<Boss>();
                for (int i = 0; i < count; i++)
                {
                    Boss boss = new Boss
                    {
                        Name = GenerateRandomString(18),
                        DifficultyLevel = random.Next(1, 101)
                    };

                    newBosses.Add(boss);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new UpdateOnlyBosses.Command { Bosses = bosses, NewData = newBosses });
                stopwatch.Stop();
                updateDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }

            for (int j = 0; j < 3; j++)
            {
                List<Equipment> equipments = await Mediator.Send(new GetEquipmentWoutCharacter.Query { count = count });
                List<Equipment> newEquipments = new List<Equipment>();
                for (int i = 0; i < count; i++)
                {
                    Equipment equipment = new Equipment
                    {
                        Name = GenerateRandomString(18),
                        Type = GenerateRandomString(10),
                        Statistics = random.Next(1, 1000),
                        Cost = random.Next(1, 10000),
                    };
                    newEquipments.Add(equipment);
                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new UpdateOnlyEquipments.Command { Equipments = equipments, NewData = newEquipments });
                stopwatch.Stop();
                updateDTO.AddOnlyEntity(stopwatch.ElapsedMilliseconds);
            }

            for ( int j = 0; j < 10; j++ )
            {

                List<AccountDetails> accountDetails = await Mediator.Send(new GetAccountDetailsLimit.Query { count = count});
                List<AccountDetails> newAccountDetails = new List<AccountDetails>();

                for (int i = 0; i < count; i++)
                {
                    AccountDetails details = new AccountDetails
                    {
                        Email = GenerateRandomString(20) + "@mail.com",
                        IsPremium = false,
                        //SignupDate = DateTime.Today,
                    };
                    newAccountDetails.Add(details);

                }
                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new UpdateAccountDetails.Command { AccountDetails = accountDetails, NewData = newAccountDetails });
                stopwatch.Stop();
                updateDTO.AddOneToOne(stopwatch.ElapsedMilliseconds);
            }


            for ( int j = 0; j < 5; j++ )
            {
                List<Character> characters = await Mediator.Send(new GetCharactersRandom.Query { count = count });
                List<Character> newCharacters = new List<Character>();

                for (int i = 0; i < count * 2; i++)
                {
                    Character character = new Character
                    {
                        CharacterType = "CharcterType" + random.Next(0, 1000),
                        Currency = random.Next(1, 1000),
                        //LevelProgression = 1,
                        //User = users[random.Next(0, users.Count() - 1)]
                    };
                    newCharacters.Add(character);

                }

                var stopwatch = Stopwatch.StartNew();
                await Mediator.Send(new UpdateCharacters.Command { Characters = characters, NewData = newCharacters });
                stopwatch.Stop();
                updateDTO.AddOneToMany(stopwatch.ElapsedMilliseconds);



                List<Guild> guilds = await Mediator.Send(new GetGuildsLimit.Query { count = count });
                List<Guild> newGuilds = new List<Guild>();
                for (int i = 0; i < count; i++)
                {
                    Guild guild = new Guild
                    {
                        Name = GenerateRandomString(12),
                        LevelProgression = random.Next(1, 100)
                    };

                    newGuilds.Add(guild);

                }
                Stopwatch stopwatch1 = Stopwatch.StartNew();
                List<Guild> guildList = await Mediator.Send(new UpdateGuilds.Command { Guilds = guilds, NewData = newGuilds });
                stopwatch1.Stop();
                updateDTO.AddOneToMany(stopwatch.ElapsedMilliseconds);
            }
            
            


            return updateDTO;
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
