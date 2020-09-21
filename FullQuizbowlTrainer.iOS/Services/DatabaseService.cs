using System;
using System.IO;
using Foundation;
using FullQuizbowlTrainer.Interfaces;
using FullQuizbowlTrainer.iOS.Services;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace FullQuizbowlTrainer.iOS.Services
{
    public class DatabaseService : IDBInterface
    {
        public DatabaseService()
        {
        }

        public SQLiteAsyncConnection CreateConnection()
        {
            var sqliteFilename = "QAData2.db";

            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string path = Path.Combine(libFolder, sqliteFilename);

            // This is where we copy in the pre-created database
            if (!File.Exists(path))
            {
                var existingDb = NSBundle.MainBundle.PathForResource("QAData2", "db");
                File.Copy(existingDb, path);
            }
            var connection = new SQLiteAsyncConnection(path);

            // Return the database connection 
            return connection;
        }
    }
}
