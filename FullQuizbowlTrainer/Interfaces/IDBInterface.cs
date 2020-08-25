using SQLite;

namespace FullQuizbowlTrainer.Interfaces
{
    public interface IDBInterface
    {
        SQLiteAsyncConnection CreateConnection();
    }
}
