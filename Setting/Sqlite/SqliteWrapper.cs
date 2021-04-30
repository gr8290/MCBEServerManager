using Dapper;
using Dapper.Contrib.Extensions;
using Setting.Sqlite.Querys;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Setting.Sqlite
{
    public class SqliteWrapper : IDisposable
    {
        private SQLiteConnection _sQLiteConnection = null;

        private bool IsBeginTran = false;

        private SQLiteTransaction sQLiteTransaction = null;

        public SqliteWrapper(bool isBeginTran = false)
        {
            ConnectionDB();
            Open();
            if (isBeginTran)
            {
                sQLiteTransaction = BeginTran();
            }
        }

        private void ConnectionDB()
        {
            _sQLiteConnection = new SQLiteConnection("Data Source=setting.sqlite;");
        }

        internal void CreateTable(string tableName)
        {
            if (CreateTableQuery.GetAllQueryList().TryGetValue(tableName, out string val))
            {
                ExecuteQuerys(val);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        internal void DropTable(string tableName)
        {
            if (DropTableQuery.GetAllQueryList().TryGetValue(tableName, out string val))
            {
                ExecuteQuerys(val);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private void Open()
        {
            _sQLiteConnection.Open();
        }

        private void Close()
        {
            RollBack();
            _sQLiteConnection.Close();
        }

        public void Dispose()
        {
            Close();
        }

        private SQLiteTransaction BeginTran()
        {
            var tran = _sQLiteConnection.BeginTransaction();
            IsBeginTran = true;
            return tran;
        }

        public bool Commit()
        {
            if (!IsBeginTran)
            {
                return false;
            }
            sQLiteTransaction.Commit();
            sQLiteTransaction.Dispose();
            IsBeginTran = false;
            return true;
        }

        public bool RollBack()
        {
            if (!IsBeginTran)
            {
                return false;
            }
            sQLiteTransaction.Rollback();
            sQLiteTransaction.Dispose();
            IsBeginTran = false;
            return true;
        }

        public IEnumerable<Table> ExecuteSelect<Table>() where Table : class
        {
            return _sQLiteConnection.GetAll<Table>();
        }

        public IEnumerable<Table> ExecuteNotKeySelect<Table>() where Table : class
        {
            string tableName = TableInfo.GetTableName(typeof(Table));
            return _sQLiteConnection.Query<Table>($"select * from {tableName}");
        }

        public Table ExecuteSelectOne<Table>(int id) where Table : class
        {
            return _sQLiteConnection.Get<Table>(id);
        }

        public bool ExecuteUpdata<Table>(Table table) where Table : class
        {
            SqlMapperExtensions.Update(_sQLiteConnection, table);
            return true;
        }

        public bool ExecuteInsert<Table>(Table table) where Table : class
        {
            _sQLiteConnection.Insert(table);
            return true;
        }

        public bool ExecuteDelete<Table>(Table table) where Table : class
        {
            _sQLiteConnection.Delete(table);
            return true;
        }

        private void ExecuteQuerys(string query)
        {
            SQLiteCommand com = new SQLiteCommand(query, _sQLiteConnection);
            com.ExecuteNonQuery();
        }

    }
}
