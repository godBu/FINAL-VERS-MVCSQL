using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperCRUDAPI.Models
{
    public class MovieRepository
    {
        private string connectionString = @"User ID = sa;Password = Password12345;Initial catalog= Movies;Data Source=localhost, 1433;  ";


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }


        public void Add(Movie newMovie)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"EXEC MovieAdd MovieID = @MovieID, Title = @Title, Genre = @Genre, Rating = @Rating, ReleaseDate = @ReleaseDate, IMDbScore = @IMDbScore";
                dbConnection.Open();
                dbConnection.Execute(sQuery, newMovie);
            }
        }



        public IEnumerable<Movie> GetAllMovies()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"EXEC MovieViewAll";
                dbConnection.Open();
                return dbConnection.Query<Movie>(sQuery);
            }
        }



        public Movie GetByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"EXEC ViewMovieByID @MovieID=@id";
                dbConnection.Open();
                return dbConnection.Query<Movie>(sQuery, new { Id = id }).FirstOrDefault();
            }

        }

        public void AddMovie(Movie newMovie)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"EXEC MovieAdd @MovieID = @MovieID, @Title = @Title, @Genre = @Genre, @Rating = @Rating, @ReleaseDate = @ReleaseDate, @IMDbScore = @IMDbScore";
                dbConnection.Open();
                dbConnection.Execute(sQuery, newMovie);
            }
        }

        public void UpdateMovies(Movie newMovie)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string linkToDB = @"EXEC MovieUpdate @MovieID = @MovieID, @Title = @Title, @Genre = @Genre, @Rating = @Rating, @ReleaseDate = @ReleaseDate, @IMDbScore = @IMDbScore";
                dbConnection.Open();
                dbConnection.Query(linkToDB, newMovie);
            }
        }

        public void DeleteByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"EXEC DeleteMovieByID @MovieID=@id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
    }
}
