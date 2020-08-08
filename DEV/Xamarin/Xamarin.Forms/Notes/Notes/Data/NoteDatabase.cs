﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data
{
    public class NoteDatabase
    {
        //一个到SQLite数据库的异步连接池
        readonly SQLiteAsyncConnection connection;
        public NoteDatabase(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
            connection.CreateTableAsync<NoteData>().Wait();
        }

        #region CRUD

        public Task<List<NoteData>> GetNotesAsync()
        {
            var data= connection.Table<NoteData>().ToListAsync();
            return data;
        }
        public Task<NoteData> GetNoteAsyncById(int id)
        {
            return connection.Table<NoteData>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<List<NoteData>> GetNoteAsyncByString(string text)
        {
            return connection.Table<NoteData>()
                            .Where(i => i.Text.Contains(text))
                            .ToListAsync();
        }

        public Task<int> UpdateNoteAsync(NoteData note)
        {
            if (note.ID != 0)
            {
                return connection.UpdateAsync(note);
            }
            else
            {
                return connection.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(NoteData note)
        {
            return connection.DeleteAsync(note);
        }

        #endregion
    }
}
