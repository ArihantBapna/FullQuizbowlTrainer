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

    }
}
