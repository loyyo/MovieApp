using ProjektProgramowanie.DAL.Entities;
using ProjektProgramowanie.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Models
{
    internal class Baza
    {
        public List<Profile> _profiles { get; set; }
        public List<MovieItem> _moviesList { get; set; }

        public Baza()
        {
            ////////////////////////////// ------------------------------ MOVIES ------------------------------ //////////////////////////////

            var movies = RepositoryMovie.GetAllMovies();
            var genres = RepositoryGenre.GetAllGenres();
            var directors = RepositoryDirector.GetAllDirectors();
            var writers = RepositoryWriter.GetAllWriters();

            _moviesList = new List<MovieItem>();
            foreach (var m in movies)
            {
                string genre = genres.Where(g => g.Id == m.Id_genre).First().genre;
                string director = directors.Where(d => d.Id == m.Id_director).First().director;
                string writer = writers.Where(w => w.Id == m.Id_writer).First().writer;
                MovieItem movie = new MovieItem(m.Title, m.Description, m.Rating, m.Year, m.Length, genre, director, writer);
                _moviesList.Add(movie);
            }

            ////////////////////////////// ------------------------------ USERS ------------------------------ //////////////////////////////

            var users = RepositoryUser.GetAllUsers();
            var addedMovies = RepositoryAddedMovies.GetAllAddedMovies();
            var auth = RepositoryAuth.GetAllAuth();
            var reviews = RepositoryReview.GetAllReviews();

            _profiles = new List<Profile>();
            foreach (var u in users)
            {
                string username = auth.Where(a => a.Id == u.Id_auth).First().Username;
                string email = auth.Where(a => a.Id == u.Id_auth).First().Email;
                string password = auth.Where(a => a.Id == u.Id_auth).First().Password;

                List<HistoryItem> userAddedMovies = new List<HistoryItem>();
                foreach (var add in addedMovies.Where(am => am.Id_user == u.Id))
                {
                    Movie addm = movies.Where(adm => adm.Id == add.Id_movie).First();
                    string genre = genres.Where(g => g.Id == addm.Id_genre).First().genre;

                    string whichlist;
                    if (add.List == false) whichlist = "PTW";
                    else whichlist = "Watched";

                    int score;
                    try { score = reviews.Where(r => r.Id_user == u.Id).Where(r => r.Id_movie == addm.Id).First().Rate; }
                    catch (Exception ex) { score = 0; }

                    HistoryItem aMovie = new HistoryItem(addm.Title, addm.Description, score, addm.Year, addm.Length, genre, whichlist, add.Date);
                    userAddedMovies.Add(aMovie);
                }

                Profile profil = new Profile(u.Name, u.Last_Name, username, email, password, u.Birthday, u.Country, u.Description, userAddedMovies, u.Id);
                _profiles.Add(profil);
            }

        }
    }
}
