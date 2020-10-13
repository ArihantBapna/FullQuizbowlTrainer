using System.Collections.Generic;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Interfaces;
using FullQuizbowlTrainer.Models;
using SQLite;
using Xamarin.Forms;

namespace FullQuizbowlTrainer.Services.Database
{
    public class DatabaseManager
    {
        SQLiteAsyncConnection dbConnection;
        public DatabaseManager()
        {
            dbConnection = DependencyService.Get<IDBInterface>().CreateConnection();
        }

        public async Task<List<Answers>> GetAnswers()
        {
            // 1.5s response delay
            return await dbConnection.Table<Answers>().ToListAsync();
        }

        public async Task<List<Questions>> GetQuestionsFromAnswerId(int answerId)
        {
            return await dbConnection.QueryAsync<Questions>("Select * From [Questions] Where [ANSWERID] = " + answerId);
        }

        public async Task<List<Categories>> GetCategoryData()
        {
            return await dbConnection.QueryAsync<Categories>("Select * From [Categories]");
        }

        public async Task<List<Answered>> GetAllAnswereds()
        {
            return await dbConnection.QueryAsync<Answered>("Select * From [Answered]");
        }

        public async Task<int> UpdateAnswer(Answers answer)
        {
            return await dbConnection.ExecuteAsync("Update Answers SET Rating = ?, Score = ?, Corrects = ?, Negs = ? Where ID = ?", answer.Rating, answer.Score, answer.Corrects, answer.Negs, answer.ID);
        }

        public async Task<int> UpdateUserCategory(Categories categery)
        {
            return await dbConnection.ExecuteAsync("UPDATE Categories SET User = ? Where Id = ?", categery.User, categery.Id);
        }

        public async Task<int> UpdateQuestion(Questions questions)
        {
            return await dbConnection.ExecuteAsync("UPDATE Questions SET Answered = ? Where ID = ?", questions.Answered, questions.ID);
        }

        public async Task<int> InsertAnsweredRead(Answered answered)
        {
            return await dbConnection.ExecuteAsync("INSERT INTO Answered (AnswerId, Answer, Category, Difficulty, Rating, Negs, Score, Correct, Clue) VALUES (?,?,?,?,?,?,?,?,?)", answered.AnswerID, answered.Answer, answered.Category, answered.Difficulty, answered.Rating, answered.Negs, answered.Score, answered.Correct, answered.Clue);
        }

    }
}
