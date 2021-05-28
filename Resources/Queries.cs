using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIDemo.Resources
{
    public static class Queries
    {
        public static string GetQuery(int number)
        {
            string query = string.Empty;
            switch (number)
            {
                case 1:
                    query = "Select * from Student";
                    break;
                case 2:
                    query = "Select * from Student where ID={0}";
                    break;
                case 3:
                    query = "select  MAX(ID)+1 NextID  from Student";
                    break;
                case 100:
                    query = "Insert Into Student (name,dob,score) values ('{0}','{1}',{2})";
                    break;
                case 200:

                    query = @"UPDATE student
                                SET Name = CASE WHEN '{name}' IS NULL THEN Name ELSE '{name}' END, 
                                  DOB = CASE WHEN '{dob}' IS NULL THEN DOB ELSE '{dob}' END, 
                                  SCORE = CASE WHEN {score} IS NULL THEN SCORE ELSE {score} END,
                                LastUpdatedDateTime=GETDATE()
                                  WHERE ID = {id}";
                    break;
                case 300:
                    query = "delete from Student where id={0}";
                    break;
                default:
                    query = string.Empty;
                    break;
            }

            return query;
        }
    }
}
