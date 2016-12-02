﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data.SQLite;

namespace Radio
{
    class Database
    {
        #region Database File Path

        public static readonly string CurrentPath = Directory.GetCurrentDirectory() + "\\";
        const string _DBFile = "favorites.sqlite";
        static readonly string _fullDBPath = CurrentPath + _DBFile;

        #endregion // Database File Path

        #region Constructors


        #endregion // Constructors

        #region Columns & Table

        const string Table = "Music";
        const string Song_ID = "Song_ID";
        const string Name = "Name";
        const string Favorite = "Favorite";

        #endregion // Columns & Table

        #region Parameters

        const string Song = "@Song";
        const string Fav = "@Fav";
        const string ID = "@ID";

        #endregion // Parameters

        #region Database Operations
        public static bool ExistsDB()
        {
            return File.Exists(_fullDBPath);
        }

        /// <summary>
        /// Creates the database and the table.
        /// </summary>
        /// <returns></returns>
        public static Task CreateDBFileAndTableAsync()
        {

            Task t = Task.Run(async () =>
            {
                SQLiteConnection.CreateFile(_fullDBPath);
                var dbConn = CreateDBConnection();
                dbConn.Open();

                await CreateTableAsync(dbConn);

                dbConn.Close();
            });

            return t;
        }

        /// <summary>
        /// Creates the DB connection. It's user's responsibility to Open and Close this connection.
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection CreateDBConnection()
        {
            return new SQLiteConnection("Data Source=" + _fullDBPath + ";Version=3;");

            // Exception in Open(): Data Source cannot be empty.  Use :memory: to open an in-memory database
            //m_DBConnection = new SQLiteConnection(currentPath  + "=" + DBFile + ";Version=3;");
        }

        /// <summary>
        /// Insert's a new song favorite. The column Favorite is set to True by default.
        /// </summary>
        /// <param name="name">Song's name.</param>
        /// <param name="conn">An opened DB connection.</param>
        public static Task InsertRecordAsync(string name, SQLiteConnection conn)
        {
            Task t = Task.Run(async () => {
                var cmd = String.Format("INSERT INTO {0} ({1}, {2}) VALUES({3}, {4})",
                Table, Name, Favorite, Song, Fav);
                var sqliteCMD = new SQLiteCommand(cmd, conn);
                sqliteCMD.Parameters.Add(new SQLiteParameter(Song, name));
                sqliteCMD.Parameters.Add(new SQLiteParameter(Fav, 1));
                await sqliteCMD.ExecuteNonQueryAsync();
            });

        return t;
    }

        /// <summary>
        /// Updates the song's Favorite column.
        /// </summary>
        /// <param name="name">Song's name.</param>
        /// <param name="favorite">Favorite boolean.</param>
        /// <param name="conn">An opened DB connection.</param>
        public static Task UpdateRecordAsync(string name, bool favorite, SQLiteConnection conn)
        {
            Task t = Task.Run(async () => {
                var cmd = String.Format("UPDATE {0} SET {1} = {2} WHERE {3} = {4}",
                Table, Favorite, Fav, Name, Song);
                var sqliteCMD = new SQLiteCommand(cmd, conn);
                sqliteCMD.Parameters.Add(new SQLiteParameter(Song, name));
                sqliteCMD.Parameters.Add(new SQLiteParameter(Fav, (favorite) ? 1 : 0));
                await sqliteCMD.ExecuteNonQueryAsync();
            });

            return t;
        }

        /// <summary>
        /// Checks if the song name exists in the DB and the Favorite column is true.
        /// </summary>
        /// <param name="name">Song's name.</param>
        /// <param name="conn">An opened DB connection.</param>
        /// <returns></returns>
        public static async Task<bool> ExistsRecordAndIsFavoriteAsync(string name, SQLiteConnection conn)
        {

            var cmd = String.Format("SELECT * FROM {0} WHERE {1} = {2}",
                Table, Name, Song);
            var sqliteCMD = new SQLiteCommand(cmd, conn);
            sqliteCMD.Parameters.Add(new SQLiteParameter(Song, name));
            var reader = await sqliteCMD.ExecuteReaderAsync();

            if (reader.Read())
            {
                if ((bool)reader[Favorite])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the record exists based on name.
        /// </summary>
        /// <param name="name">Song's name.</param>
        /// <param name="conn">An opened DB connection.</param>
        /// <returns></returns>
        public static async Task<bool> ExistsRecordAsync(string name, SQLiteConnection conn)
        {
            var cmd = String.Format("SELECT * FROM {0} WHERE {1} = {2}",
                Table, Name, Song);
            var sqliteCMD = new SQLiteCommand(cmd, conn);
            sqliteCMD.Parameters.Add(new SQLiteParameter(Song, name));
            var reader = await sqliteCMD.ExecuteReaderAsync();

            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a record based in name.
        /// </summary>
        /// <param name="name">Song's name.</param>
        /// <param name="conn">An opened DB connection.</param>
        public static async void DeleteRecordAsync(string name, SQLiteConnection conn)
        {
            var cmd = String.Format("DELETE FROM {0} WHERE {1} = {2}",
                Table, Name, Song);
            var sqliteCMD = new SQLiteCommand(cmd, conn);
            sqliteCMD.Parameters.Add(new SQLiteParameter(Song, name));
            await sqliteCMD.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Deletes a record based in the ID.
        /// </summary>
        /// <param name="currID">Curent ID.</param>
        /// <param name="conn">An opened DB connection.</param>
        public static async void DeleteRecordAsync(int currID, SQLiteConnection conn)
        {
            var cmd = String.Format("DELETE FROM {0} WHERE {1} = {2}",
                Table, Song_ID, ID);
            var sqliteCMD = new SQLiteCommand(cmd, conn);
            sqliteCMD.Parameters.Add(new SQLiteParameter(ID, currID));
            await sqliteCMD.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Creates the main table.
        /// </summary>
        /// <param name="conn">An opened DB connection.</param>
        public static Task CreateTableAsync(SQLiteConnection conn)
        {
            Task t = Task.Run(async () => {
                var cmd = String.Format("CREATE TABLE {0} ({1} INTEGER PRIMARY KEY AUTOINCREMENT, {2} TEXT, {3} BOOL)",
                Table, Song_ID, Name, Favorite);
                var sqliteCMD = new SQLiteCommand(cmd, conn);
                await sqliteCMD.ExecuteNonQueryAsync();
            });

            return t;
        }

        public static async void PrintAllRecordsAsync(SQLiteConnection conn)
        {
            var sqlSelect = String.Format("SELECT * FROM {0}", Table);
            var sqliteCMD = new SQLiteCommand(sqlSelect, conn);
            var reader = await sqliteCMD.ExecuteReaderAsync();

            while (reader.Read())
            {
                Console.WriteLine("ID: " + reader[Song_ID] + "\tSong: " + reader[Name] + "\tFavorite: " + reader[Favorite]);
            }
        }

        #endregion // Database Operations
    }
}
