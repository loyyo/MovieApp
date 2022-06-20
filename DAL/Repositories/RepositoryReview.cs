using MySqlConnector;
using ProjektProgramowanie.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektProgramowanie.DAL.Repositories
{
    internal class RepositoryReview
    {
        private const string ALL_REVIEWS = "SELECT * FROM review";
        private const string ADD_REVIEW = "INSERT INTO `review`(`Id_movie`,`Id_user`,`Rate`, `Comment`) VALUES ";

        public static List<Review> GetAllReviews()
        {
            List<Review> review = new List<Review>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_REVIEWS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    review.Add(new Review(reader));
                connection.Close();

            }
            return review;
        }

        public static bool AddReviewToDB(Review review)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_REVIEW} {review.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                review.Id = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        public static bool DeleteReviewFromDB(int reviewID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_REVIEW = $"DELETE FROM review WHERE Id={reviewID}";

                MySqlCommand command = new MySqlCommand(EDIT_REVIEW, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }

        public static bool EditReviewInDB(Review review, int reviewID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_REVIEW = $"UPDATE review SET Id_movie={review.Id_movie}, Id_user={review.Id_user}, " +
                    $"Rate={review.Rate} WHERE Id={reviewID}";

                MySqlCommand command = new MySqlCommand(EDIT_REVIEW, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }
    }
}
