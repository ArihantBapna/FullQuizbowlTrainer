using System;
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

        public async Task<List<AnswersModel>> GetAllAnswers()
        {
            //return await dbConnection.QueryAsync<AnswersModel>("Select * From [Answers]");
            return await dbConnection.Table<AnswersModel>().ToListAsync();
        }

        public async Task<List<QuestionsModel>> GetAllQuestions()
        {
            return await dbConnection.QueryAsync<QuestionsModel>("Select * From [Questions]");
        }

        public async Task<int> SaveEmployeeAsync(QuestionsModel q)

        {
            return await dbConnection.InsertAsync(q);
        }
    }
}
