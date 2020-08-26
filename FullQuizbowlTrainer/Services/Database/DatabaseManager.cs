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

        public async Task<List<Answers>> GetAllAnswers()
        {
            //Query for this takes about 1.5s on avg. 
            return await dbConnection.Table<Answers>().ToListAsync();
        }

        public async Task<List<Questions>> GetAllQuestions()
        {
            return await dbConnection.QueryAsync<Questions>("Select * From [Questions]");
        }

        public async Task<int> SaveEmployeeAsync(Questions q)

        {
            return await dbConnection.InsertAsync(q);
        }
    }
}
