using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBazaar.DALC
{
    public static class SqlQueries
    {
        public static class Category
        {
            public const string GetAll = @"SELECT category_id, name, display_order, description, parent_id,
                                                  SUBSTR(SYS_CONNECT_BY_PATH(name,' >> '), 5) category_path 
                                             FROM  categories 
                                            WHERE is_deleted = 0 
                                            START WITH parent_id IS NULL
                                          CONNECT BY PRIOR category_id = parent_id  
                                            ORDER SIBLINGS BY display_order ASC, created_on DESC";
            public const string GetByID = @"SELECT category_id, name, parent_id, description, display_order
                                                                    FROM categories WHERE category_id=:id";
            public const string Create = @"INSERT INTO CATEGORIES (name,parent_id,description,display_order,image_id) 
                                                       VALUES (:name,:parent_id,:description,:display_order,1)";
            public const string Update = @"UPDATE categories SET name=:name, parent_id=:parent_id,description=:description,
                                                               display_order=:display_order, updated_on = SYSDATE WHERE category_id=:category_id";
            public const string CheckChildExists = "SELECT COUNT(*) FROM categories WHERE parent_id=:id AND is_deleted = 0";

            public const string Delete = "UPDATE categories SET is_deleted = 1,updated_on = SYSDATE  WHERE category_id=:id";

            public const string CheckNameExists = "SELECT category_id FROM categories WHERE name=:name AND is_deleted = 0 ";
        }
    }
}