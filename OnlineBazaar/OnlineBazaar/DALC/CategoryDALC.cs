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

        public static CategoryModel GetById(int id)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.GetByID;
                    cmd.BindByName = true;
                    cmd.Parameters.Add(new OracleParameter("id", id));
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(dt);
                }
            }
            return dt.AsEnumerable().Select(row => new CategoryModel(row)).SingleOrDefault();
        }

        public static bool Create(CategoryModel model)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.Create;
                    cmd.BindByName = true;
                    OracleParameter[] oracleParameters = new OracleParameter[]
                    {
                        new OracleParameter("NAME",model.Name),
                        new OracleParameter("PARENT_ID",model.ParentId),
                        new OracleParameter("DESCRIPTION",model.Description),
                        new OracleParameter("DISPLAY_ORDER",model.DisplayOrder)
                    };
                    cmd.Parameters.AddRange(oracleParameters);
                    var result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
        public static bool Update(CategoryModel model)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.Update;
                    cmd.BindByName = true;
                    OracleParameter[] oracleParams = new OracleParameter[] {
                        new OracleParameter("NAME",model.Name),
                        new OracleParameter("PARENT_ID",model.ParentId),
                        new OracleParameter("DESCRIPTION",model.Description),
                        new OracleParameter("DISPLAY_ORDER",model.DisplayOrder),
                        new OracleParameter("CATEGORY_ID",model.Id)
                    };
                    cmd.Parameters.AddRange(oracleParams);
                    var result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
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
                    cmd.Parameters.Add(new OracleParameter("ID", id));
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
                    cmd.Parameters.Add(new OracleParameter("ID", id));
                    int deletedRows = 0;
                    deletedRows = cmd.ExecuteNonQuery();
                    result = deletedRows == 1;
                }
            }
            return result;
        }

        public static bool CheckNameExists(string name, int id)
        {
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.CheckNameExists;
                    cmd.Parameters.Add(new OracleParameter("NAME", name));
                    cmd.BindByName = true;
                    int result = Convert.ToInt32((cmd.ExecuteScalar()) ?? 0);
                    if (result == id)
                    {
                        return false;
                    }
                    return result > 0;
                }
            }

        }
    }
}