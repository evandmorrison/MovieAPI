using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Repositories;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        this.movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetAll()
    {
        return await this.movieRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> Get([FromRoute] int id)
    {
        return await this.movieRepository.GetByIdAsync(id);
    }

    [HttpGet("by-title/{title}")]
    public async Task<ActionResult<Movie>> GetByTitle([FromRoute] string title)
    {
        return await this.movieRepository.GetByTitleAsync(title);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Post([FromBody] Movie movie)
    {
        return await this.movieRepository.CreateAsync(movie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }

        await movieRepository.UpdateAsync(id, movie);

        return NoContent();
    }

    [HttpPut("like-movie/{id}")]
    public async Task<IActionResult> LikeMovie([FromRoute] int id, [FromBody] Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }
        movie.Rating++;

        await movieRepository.UpdateAsync(id, movie);

        return NoContent();
    }

    [HttpPut("dislike-movie/{id}")]
    public async Task<IActionResult> DislikeMovie([FromRoute] int id, [FromBody] Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }

        movie.Rating--;
        await movieRepository.UpdateAsync(id, movie);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Movie>> Delete([FromRoute] int id)
    {
        return await movieRepository.DeleteAsync(id);
    }
}