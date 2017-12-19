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
        public static CategoryModel GetByID(int ID)
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
                    cmd.Parameters.Add(new OracleParameter("ID", ID));
                    OracleDataAdapter oda = new OracleDataAdapter(cmd);
                    oda.Fill(dt);
                }
            }
            return dt.AsEnumerable().Select(row => new CategoryModel(row)).FirstOrDefault();
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
                        new OracleParameter("name",model.Name),
                        new OracleParameter("parent_id",model.ParentID),
                        new OracleParameter("description",model.Description),
                        new OracleParameter("display_order",model.DisplayOrder)
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
                        new OracleParameter("name",model.Name),
                        new OracleParameter("parent_id",model.ParentID),
                        new OracleParameter("description",model.Description),
                        new OracleParameter("display_order",model.DisplayOrder),
                        new OracleParameter("category_id",model.ID)
                    };
                    cmd.Parameters.AddRange(oracleParams);
                    var result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public static List<SelectListItem> GetParents()
        {
            List<SelectListItem> parents = new List<SelectListItem>();
            using (OracleConnection con = new OracleConnection(AppConfig.ConnectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = SqlQueries.Category.GetParents;
                    OracleDataReader reader = cmd.ExecuteReader();
                    parents.Add(new SelectListItem
                    {
                        Text = "None",
                        Value = ""
                    });
                    while (reader.Read())
                    {
                        parents.Add(new SelectListItem
                        {
                            Text = reader["Path"].ToString(),
                            Value = reader["category_id"].ToString(),
                        });
                    }
                }
            }
            return parents;
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
                    cmd.Parameters.Add(new OracleParameter("name", name));
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