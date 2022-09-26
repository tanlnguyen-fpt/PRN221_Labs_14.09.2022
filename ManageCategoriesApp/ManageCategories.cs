using Npgsql;
using System;
using System.Collections.Generic;

namespace ManageCategoriesApp
{
    public record Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class ManageCategories
    {
        string connString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=MyStore";
        NpgsqlConnection conn;

        public List<Category> GetCategories()
        {
            List<Category> categories = new();
            conn = new(connString);
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new("Select CategoryId, CategoryName from Categories", conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category { CategoryID = reader.GetInt32(0), CategoryName = reader.GetString(1) });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return categories;
        }

        public void InsertCategory(Category category)
        {
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new("Insert into Categories(categoryname) values (@CategoryName)", conn);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new("Update Categories set categoryname=@CategoryName where CategoryId=@CategoryId", conn);
                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DeleteCategory(Category category)
        {
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new("Delete from Categories where CategoryId=@CategoryId", conn);
                cmd.Parameters.AddWithValue("@CategoryId", category.CategoryID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
