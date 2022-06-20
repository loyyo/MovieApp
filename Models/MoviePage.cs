using MovieApp.DAL.Entities;
using MovieApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieApp.Models
{
    internal class MoviePage
    {
        public string GetSelectedScore(int Score)
        {
            string SelectedScore = "";
            if (Score == 0) SelectedScore = "Select";
            else
            {
                switch (Score)
                {
                    case 1:
                        SelectedScore = "(1) Appalling";
                        break;
                    case 2:
                        SelectedScore = "(2) Horrible";
                        break;
                    case 3:
                        SelectedScore = "(3) Very Bad";
                        break;
                    case 4:
                        SelectedScore = "(4) Bad";
                        break;
                    case 5:
                        SelectedScore = "(5) Average";
                        break;
                    case 6:
                        SelectedScore = "(6) Fine";
                        break;
                    case 7:
                        SelectedScore = "(7) Good";
                        break;
                    case 8:
                        SelectedScore = "(8) Very Good";
                        break;
                    case 9:
                        SelectedScore = "(9) Great";
                        break;
                    case 10:
                        SelectedScore = "(10) Masterpiece";
                        break;
                }
            }
            return SelectedScore;
        }

        public (HistoryItem, bool) GetProfileMovie(Profile profil, MovieItem movie)
        {

            bool DeleteEnabled = false;
            HistoryItem _profileMovie = new HistoryItem(string.Empty, string.Empty, 0, string.Empty, 0, string.Empty, string.Empty, string.Empty);
            try
            {
                _profileMovie = profil.AddedMovies.Where(x => x.MovieName == movie.MovieName).First();
                DeleteEnabled = true;
            }
            catch (Exception ex) {}

            return (_profileMovie, DeleteEnabled);
        }

        public bool Delete(Profile profil, HistoryItem _profileMovie, MovieItem movie)
        {
            bool success = false;
            int idAddedMovie = RepositoryAddedMovies.GetAllAddedMovies().Where(i => i.Id_movie == movie.ID).Where(i => i.Id_user == profil.ID).First().Id;
            if (RepositoryAddedMovies.DeleteAddedMovieFromDB(idAddedMovie))
            {
                int idReview = RepositoryReview.GetAllReviews().Where(i => i.Id_movie == movie.ID).Where(i => i.Id_user == profil.ID).First().Id;
                if (success = RepositoryReview.DeleteReviewFromDB(idReview))
                {
                    profil.AddedMovies.Remove(_profileMovie);
                    MessageBox.Show("Movie successfully deleted from list", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return !success;
        }

        public void ExecuteAddToListCommand(string ListName, string SelectedScore, string MovieName, string Description, string Year,
            int Length, string Genre, Profile profile, MovieItem movie, ICommand GoToProfileView, HistoryItem _profileMovie)
        {
            int YourScore = 0;
            switch (SelectedScore)
            {
                case "Select":
                    YourScore = 0;
                    break;
                case "(1) Appalling":
                    YourScore = 1;
                    break;
                case "(2) Horrible":
                    YourScore = 2;
                    break;
                case "(3) Very Bad":
                    YourScore = 3;
                    break;
                case "(4) Bad":
                    YourScore = 4;
                    break;
                case "(5) Average":
                    YourScore = 5;
                    break;
                case "(6) Fine":
                    YourScore = 6;
                    break;
                case "(7) Good":
                    YourScore = 7;
                    break;
                case "(8) Very Good":
                    YourScore = 8;
                    break;
                case "(9) Great":
                    YourScore = 9;
                    break;
                case "(10) Masterpiece":
                    YourScore = 10;
                    break;
            }

            HistoryItem historyItem = new HistoryItem(
                MovieName,
                Description,
                YourScore,
                Year,
                Length,
                Genre,
                ListName,
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString()
                );

            int listInt = 0;
            if (ListName == "Watched") listInt = 1;
            string[] splitDate = historyItem.Date.Split(" ");
            string[] shortDate = splitDate[0].Split(".");
            string correctDate = shortDate[2] + "-" + shortDate[1] + "-" + shortDate[0] + " " + splitDate[1];

            AddedMovies addedMovie = new AddedMovies(profile.ID, movie.ID, correctDate, listInt);
            Review review = new Review(movie.ID, profile.ID, YourScore);

            if (!profile.AddedMovies.Contains(_profileMovie))
            {
                if (RepositoryAddedMovies.AddAddedMovieToDB(addedMovie))
                {
                    if (RepositoryReview.AddReviewToDB(review))
                    {
                        profile.AddedMovies.Add(historyItem);
                        MessageBox.Show("Movie successfully added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        GoToProfileView.Execute(this);
                    }
                }
            }

            else if (profile.AddedMovies.Contains(_profileMovie))
            {
                int idAddedMovie = RepositoryAddedMovies.GetAllAddedMovies().Where(i => i.Id_movie == movie.ID).Where(i => i.Id_user == profile.ID).First().Id;
                if (RepositoryAddedMovies.EditAddedMovieInDB(addedMovie, idAddedMovie))
                {
                    int idReview = RepositoryReview.GetAllReviews().Where(i => i.Id_movie == movie.ID).Where(i => i.Id_user == profile.ID).First().Id;
                    if (RepositoryReview.EditReviewInDB(review, idReview))
                    {
                        profile.AddedMovies.Remove(_profileMovie);
                        profile.AddedMovies.Add(historyItem);
                        MessageBox.Show("Score successfully changed", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        GoToProfileView.Execute(this);
                    }
                }
            }
        }
    }
}
