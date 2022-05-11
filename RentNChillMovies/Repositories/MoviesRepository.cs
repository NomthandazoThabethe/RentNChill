using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Models;
using RentNChillMovies.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment host;

        public MoviesRepository(ApplicationDbContext dbContext, IWebHostEnvironment host) 
        {
            this.dbContext = dbContext;
            this.host = host;
        }

        public Movie GetMovies(int movieId)
        {
            return dbContext.Movies.FirstOrDefault(b => b.MovieId == movieId);
        }

        public  async Task PostNewMovie(MovieViewModel movieViewModel)
        {
            if (movieViewModel.Movie == null)
            {
                throw new ArgumentNullException(nameof(movieViewModel.Movie));
            }

            if (movieViewModel != null)
            {
                //adding Image
                string wwwRootPath = host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(movieViewModel.Movie.ImageFile.FileName);
                string extension = Path.GetExtension(movieViewModel.Movie.ImageFile.FileName);
                fileName = DateTime.Now.ToString("yymmssffff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/movieThumbnails/", fileName);
                movieViewModel.Movie.MovieThumbnail = "/images/movieThumbnails/" + fileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    movieViewModel.Movie.ImageFile.CopyTo(fileStream);
                }

                if (movieViewModel.Movie.MovieTrailer.Any()||movieViewModel.Movie.MovieTrailer.Equals(null))
                {
                     if (movieViewModel.Movie.MovieTrailer.Contains("https://youtu.be/") || movieViewModel.Movie.MovieTrailer.Contains("https://youtube/"))
                    {
                        var newLink = "https://www.youtube.com/embed/";

                        movieViewModel.Movie.MovieTrailer = newLink + movieViewModel.Movie.MovieTrailer.Substring(movieViewModel.Movie.MovieTrailer.Length - 11);

                    }
                    else
                    {
                        movieViewModel.Movie.MovieTrailer = "https://www.youtube.com/embed/l60MnDJklnM";
                    }
                }
                dbContext.Add(movieViewModel.Movie);

                await dbContext.SaveChangesAsync();
                var movieId = movieViewModel.Movie.MovieId;

                if (movieViewModel.Actor.Any())
                {
                    List<MovieActor> movieActors = new List<MovieActor>();
                    foreach (var i in movieViewModel.Actor)
                    {
                        var actor = new MovieActor
                        {
                            ActorId = i,
                            MovieId = movieId
                        };
                        //adding actor to movieactors list
                        movieActors.Add(actor);
                    }
                    //adding the list
                    dbContext.MovieActors.AddRange(movieActors);
                 
                }  

                if (movieViewModel.Producer.Any())
                {
                    List<MovieProducer> movieProducers = new List<MovieProducer>();
                    foreach (var i in movieViewModel.Producer)
                    {
                        var producer = new MovieProducer
                        {
                            ProducerId = i,
                            MovieId = movieId
                        };
                        movieProducers.Add(producer);
                    }
                    dbContext.MovieProducers.AddRange(movieProducers);
                   await dbContext.SaveChangesAsync();
                }
            }

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

        }

        public async Task UpdateMovies(MovieViewModel movieViewModel)
        {
            if (movieViewModel.Movie == null) 
            {
                throw new ArgumentNullException(nameof(movieViewModel.Movie));
            }

            var movieObj = dbContext.Movies.FirstOrDefault(m => m.MovieId == movieViewModel.Movie.MovieId);

            if (movieViewModel.Movie.ImageFile != null)
            {

                //adding Image
                string wwwRootPath = host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(movieViewModel.Movie.ImageFile.FileName);
                string extension = Path.GetExtension(movieViewModel.Movie.ImageFile.FileName);
                fileName = DateTime.Now.ToString("yymmssffff") + extension;
                string path = Path.Combine(wwwRootPath + "/images/movieThumbnails/", fileName);
                movieViewModel.Movie.MovieThumbnail = "/images/movieThumbnails/" + fileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    movieViewModel.Movie.ImageFile.CopyTo(fileStream);
                }

                movieObj.MovieTitle = movieViewModel.Movie.MovieTitle;
                movieObj.MovieDescription = movieViewModel.Movie.MovieDescription;
                movieObj.Price = movieViewModel.Movie.Price;
                movieObj.MovieThumbnail =fileName;
                movieObj.ImdbURL= movieViewModel.Movie.ImdbURL;
                movieObj.RottenTomatoesURL = movieViewModel.Movie.RottenTomatoesURL;
                movieObj.GenreId = movieViewModel.Movie.GenreId;
                movieObj.Genre= movieViewModel.Movie.Genre;
                movieObj.MovieTrailer = movieViewModel.Movie.MovieTrailer;
                movieObj.ReleaseDate = movieViewModel.Movie.ReleaseDate;
                movieObj.IsAvailable = movieViewModel.Movie.IsAvailable;
                movieObj.Language = movieViewModel.Movie.Language;

                dbContext.Movies.Update(movieObj);
                var movieId = movieViewModel.Movie.MovieId;

                if (movieViewModel.Actor.Any())
                {
                    List<MovieActor> movieActors = new List<MovieActor>();
                    foreach (var i in movieViewModel.Actor)
                    {
                        var actor = new MovieActor
                        {
                            ActorId = i,
                            MovieId = movieId
                        };
                        //adding actor to movieactors list
                        movieActors.Add(actor);
                    }
                    //adding the list
                    dbContext.MovieActors.UpdateRange(movieActors);

                }

                if (movieViewModel.Producer.Any())
                {
                    List<MovieProducer> movieProducers = new List<MovieProducer>();
                    foreach (var i in movieViewModel.Producer)
                    {
                        var producer = new MovieProducer
                        {
                            ProducerId = i,
                            MovieId = movieId
                        };
                        movieProducers.Add(producer);
                    }
                    dbContext.MovieProducers.UpdateRange(movieProducers);
                    await dbContext.SaveChangesAsync();
                }
            }

            else {
                movieObj.MovieTitle = movieViewModel.Movie.MovieTitle;
                movieObj.MovieDescription = movieViewModel.Movie.MovieDescription;
                movieObj.Price = movieViewModel.Movie.Price;
                movieObj.ImdbURL = movieViewModel.Movie.ImdbURL;
                movieObj.RottenTomatoesURL= movieViewModel.Movie.RottenTomatoesURL;
                movieObj.GenreId = movieViewModel.Movie.GenreId;
                movieObj.Genre = movieViewModel.Movie.Genre;
                movieObj.MovieTrailer = movieViewModel.Movie.MovieTrailer;
                movieObj.ReleaseDate = movieViewModel.Movie.ReleaseDate;
                movieObj.IsAvailable = movieViewModel.Movie.IsAvailable;
                movieObj.Language = movieViewModel.Movie.Language;

                dbContext.Movies.Update(movieObj);
                var movieId = movieViewModel.Movie.MovieId;

                if (movieViewModel.Actor.Any())
                {
                    List<MovieActor> movieActors = new List<MovieActor>();
                    foreach (var i in movieViewModel.Actor)
                    {
                        var actor = new MovieActor
                        {
                            ActorId = i,
                            MovieId = movieId
                        };
                        //adding actor to movieactors list
                        movieActors.Add(actor);
                    }
                    //adding the list
                    dbContext.MovieActors.AddRange(movieActors);

                }

                if (movieViewModel.Producer.Any())
                {
                    List<MovieProducer> movieProducers = new List<MovieProducer>();
                    foreach (var i in movieViewModel.Producer)
                    {
                        var producer = new MovieProducer
                        {
                            ProducerId = i,
                            MovieId = movieId
                        };
                        movieProducers.Add(producer);
                    }
                    dbContext.MovieProducers.AddRange(movieProducers);
                    await dbContext.SaveChangesAsync();
                }
            }

            try
            {
               await dbContext.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException) 
            {
                throw;
            }
        }

        
    }
}
