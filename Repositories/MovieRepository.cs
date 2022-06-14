using MovieAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie> CreateAsync(Movie model);
        Task<Movie> DeleteAsync(int id);
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<int> UpdateAsync(int id, Movie model);
    }

    public class MovieRepository : IMovieRepository
    {
        public readonly IDBContext dBContext;

        public MovieRepository(IDBContext context) { this.dBContext = context; }

        public async Task<List<Movie>> GetAllAsync()
        {
            return (await this.dBContext.Movie.ToListAsync()).ToModels<Movie, MovieAPI.Database.Models.Movie>();
        }

        public async Task<Movie> GetByTitleAsync(string title)
        {
            var dataObject = await this.dBContext.Movie.FirstOrDefaultAsync(m => m.Title == title);

            if (dataObject == null)
            {
                throw new KeyNotFoundException();
            }

            return dataObject.ToModel<Movie, MovieAPI.Database.Models.Movie>();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var dataObject = await this.dBContext.Movie.FirstOrDefaultAsync(m => m.Id == id);

            if (dataObject == null)
            {
                throw new KeyNotFoundException();
            }

            return dataObject.ToModel<Movie, MovieAPI.Database.Models.Movie>();
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            movie.Id = 0;
            var dataObject = movie.ToModel<MovieAPI.Database.Models.Movie, Movie>();

            await this.dBContext.Movie.AddAsync(dataObject);
            await dBContext.SaveChangesAsync();

            return dataObject.ToModel<Movie, MovieAPI.Database.Models.Movie>();
        }

        public async Task<int> UpdateAsync(int id, Movie movie)
        {
            this.dBContext.Movie.Update(movie.ToModel<MovieAPI.Database.Models.Movie, Movie>());
            return await dBContext.SaveChangesAsync();
        }

        public async Task<Movie> DeleteAsync(int id)
        {
            var movie = await GetByIdAsync(id);

            this.dBContext.Movie.Remove(movie.ToModel<MovieAPI.Database.Models.Movie, Movie>());
            await dBContext.SaveChangesAsync();

            return movie;
        }
    }
}