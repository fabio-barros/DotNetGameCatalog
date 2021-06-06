using API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GamePsqlRepository : IGameRepository
    {
        private readonly NpgsqlConnection npgsqlConnection;

        public GamePsqlRepository(IConfiguration configuration)
        {
            npgsqlConnection = new NpgsqlConnection(configuration.GetConnectionString("DbConnectionString"));

        }

        //GET:
        public async Task<List<Game>> GetGames(int page, int qty)
        {
            var games = new List<Game>();

            var command = $"select * from Games order by Id offset {((page - 1) * qty)} rows fetch next {qty} rows only";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            while (npgsqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (int)npgsqlDataReader["Id"],
                    Title = (string)npgsqlDataReader["Title"],
                    Developer = (string)npgsqlDataReader["Developer"],
                    Publisher = (string)npgsqlDataReader["Publisher"],
                    Year = (DateTime)npgsqlDataReader["release_date"],
                    Price = (decimal)npgsqlDataReader["Price"]

                }
                );
            }

            await npgsqlConnection.CloseAsync();

            return games;

        }

        //GET:
        public async Task<Game> GetGame(int id)
        {
            Game game = null;

            var command = $"select * from Games where Id = '{id}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            while (npgsqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (int)npgsqlDataReader["Id"],
                    Title = (string)npgsqlDataReader["Title"],
                    Developer = (string)npgsqlDataReader["Developer"],
                    Publisher = (string)npgsqlDataReader["Publisher"],
                    Year = (DateTime)npgsqlDataReader["release_date"],
                    Price = (decimal)npgsqlDataReader["Price"]

                };
                
            }

            await npgsqlConnection.CloseAsync();

            return game;
        }

        //GET:
        public async Task<List<Game>> GetGame(string title, string developer)
        {
            var games = new List<Game>();

            var command = $"select * from games where Title = '{title}' and Developer = '{developer}'";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            NpgsqlDataReader npgsqlDataReader = await npgsqlCommand.ExecuteReaderAsync();

            while (npgsqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (int)npgsqlDataReader["Id"],
                    Title = (string)npgsqlDataReader["Title"],
                    Developer = (string)npgsqlDataReader["Developer"],
                    Publisher = (string)npgsqlDataReader["Publisher"],
                    Year = (DateTime)npgsqlDataReader["release_date"],
                    Price = (decimal)npgsqlDataReader["Price"]

                }
              );

            }

            await npgsqlConnection.CloseAsync();

            return games;
        }

        //POST:
        public async Task InsertGame(Game game)
        {
            var command = $"insert into games (title, developer, publisher, release_date, price) values ( '{game.Title}', '{game.Developer}', '{game.Publisher}', '{game.Year}', {game.Price})";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();

            await npgsqlConnection.CloseAsync();
        }

        public async Task UpdateGame(Game game)
        {
            var command = $"update Games set Title='{game.Title}', Developer='{game.Developer}', Publisher='{game.Publisher}', release_date='{game.Year}', Price={game.Price} where Id = {game.Id}";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();

            await npgsqlConnection.CloseAsync();
        }

        public async Task DeleteGame(int id)
        {
            var command = $"delete from games where id = {id}";

            await npgsqlConnection.OpenAsync();
            NpgsqlCommand npgsqlCommand = new(command, npgsqlConnection);
            npgsqlCommand.ExecuteNonQuery();

            await npgsqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            npgsqlConnection?.Close();
            npgsqlConnection?.Dispose();
        }
    }
}
