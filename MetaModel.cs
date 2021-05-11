using Microsoft.Win32.SafeHandles;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SQL_DSL
{
    public class Table
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }

    public class Query
    {
        public Query()
        {
            this.Selections = new List<Selection>();
            this.Conditions = new List<Condition>();
        }

        public List<Condition> Conditions { get; set; }
        public List<Selection> Selections { get; set; }
        public string TextQuery { get; set; }
        public Table Table { get; set; }

        public void AddCondition(Condition condition)
        {
            Conditions.Add(condition);
        }

        public void AddSelection(Selection selection)
        {
            Selections.Add(selection);
        }

        public void MakeQuery()
        {
            string query = "";
            query += "SELECT ";

            if(this.Selections.Count != 0)
                for(int i = 0; i < this.Selections.Count; i++)
                {
                    if (i < this.Selections.Count - 1)
                        query += this.Selections[i].Field + ", ";
                    else
                        query += this.Selections[i].Field + " ";
                }

            query += "FROM " + this.Table.Name + " ";

                query += "WHERE ";

            if(this.Conditions.Count != 0)
            {
                for(int i = 0; i < this.Conditions.Count; i++)
                {
                    if (i < this.Conditions.Count - 1)
                        query += this.Conditions[i].Item + " AND ";
                    else
                        query += this.Conditions[i].Item;
                }
            }

            query += ";";
            this.TextQuery = query;
        }

        public void PrintQuery()
        {
            this.MakeQuery();
            Console.WriteLine(this.TextQuery);
        }

        public void QueryDB()
        {
            this.MakeQuery();

            var uriString = this.Table.ConnectionString;
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                uri.Host, db, user, passwd, port);

            using var con = new NpgsqlConnection(connStr);
            con.Open();

            using var cmd = new NpgsqlCommand(this.TextQuery, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                for(int i = 0; i < rdr.FieldCount; i++)
                {
                    var value = rdr.GetValue(i);
                    Console.Write(value.ToString() + " ");
                }

                Console.WriteLine();
            }
        }
    }

    public class Selection
    {
        public string Field { get; set; }
        
        public Selection(string field)
        {
            Field = field;
        }
    }

    public class Condition
    {
        public string Item { get; set; }
        
        public Condition(String condition)
        {
            this.Item = condition;
        }
    }
}
