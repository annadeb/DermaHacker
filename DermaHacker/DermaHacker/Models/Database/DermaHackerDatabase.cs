using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DermaHacker.Models.Database
{
    public class DermaHackerDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public DermaHackerDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Report>().Wait();
        }

        public Task<List<Report>> GetReportsAsync()
        {
            return _database.Table<Report>().ToListAsync();
        }

        public Task<int> SaveReportAsync(Report report)
        {
            return _database.InsertAsync(report);
        }
    }
}
