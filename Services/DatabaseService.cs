﻿using AlbumApp.Models;
using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp.Services
{
    //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-9.0
    public class DatabaseService
    {
        public const string DatabaseFilename = "ListenedAlbums.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        //store data on phone locally
        public static string DatabasePath =>
            Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DatabaseFilename);

    }
}
