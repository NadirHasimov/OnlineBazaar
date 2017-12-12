using OnlineBazaar.DomainModels;
using OnlineBazaar.Lib;
using OnlineBazaar.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBazaar.DALC.Category
{
    public class CategoryDALC
    {
        public static List<CategoryModel> GetAll()
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.GetAll;
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(dt);
                }
            }
            return dt.AsEnumerable().Select(row => new CategoryModel(row)).ToList();
        }

        public static bool CheckChildExists(int id)
        {
            bool result;
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.CheckChildExists;
                    cmd.BindByName = true;
                    cmd.Parameters.Add(new OracleParameter("id", id));
                    result = int.Parse(cmd.ExecuteScalar().ToString()) > 0;
                }
            }
            return result;
        }
        
        public static bool Delete(int id)
        {
            bool result;
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.Delete;
                    cmd.BindByName = true;
                    cmd.Parameters.Add(new OracleParameter("id", id));
                    int deletedRows = 0;
                    try
                    {
                        deletedRows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) {
                        int a = 5;
                    }
                    result = deletedRows == 1;
                }
            }
            return result;
        }
    }
}