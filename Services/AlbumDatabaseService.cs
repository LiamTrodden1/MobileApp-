using AlbumApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AlbumApp.Services
{
    //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0
    public class AlbumDatabaseService
    {
        const string DatabaseFilename = "ListenedAlbums.db3";

        static string DatabasePath =>
            Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DatabaseFilename);

        const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        SQLiteAsyncConnection database;

        // Initialize the database connection
        async Task Init()
        {
            if (database != null)
                return;

            database = new SQLiteAsyncConnection(DatabasePath, Flags);
            await database.CreateTableAsync<AlbumManagementModel>();
        }

        // Get all database items
        public async Task<List<AlbumManagementModel>> GetItemsAsync()
        {
            await Init();
            return await database.Table<AlbumManagementModel>().ToListAsync();
        }

        // Get an item by its ID
        public async Task<AlbumManagementModel> GetItemAsync(int id)
        {
            await Init();
            return await database.Table<AlbumManagementModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        // Get an item by its albumTitle
        public async Task<AlbumManagementModel> GetItemByTitleAsync(string albumTitle)
        {
            await Init();
            return await database.Table<AlbumManagementModel>().Where(i => i.albumTitle == albumTitle).FirstOrDefaultAsync();
        }

        //get the top 3 msot recent inputs by ID
        public async Task<List<AlbumManagementModel>> GetMostRecentAlbumsAsync(int count = 3)
        {
            await Init();
            return await database.Table<AlbumManagementModel>().OrderByDescending(a => a.ID).Take(count).ToListAsync();
        }

        // Save a new or existing item
        public async Task<int> SaveItemAsync(AlbumManagementModel item)
        {
            await Init();
            if (item.ID != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }

        // Update an item in the database
        public async Task<int> UpdateItemAsync(AlbumManagementModel item)
        {
            await Init();
            return await database.UpdateAsync(item);
        }

        // Delete an item in the database
        public async Task<int> DeleteItemAsync(AlbumManagementModel item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}
