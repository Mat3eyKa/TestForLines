using TestForLines.Models;
using TestForLines.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestForLines
{
    internal class DataWorker
    {
        public List<ModelToTable> GetDataFromDb(int month, int year, string tupeOfSort)
        {
            List<ModelToTable> list = new();
            try
            {
                DataTable dt = new();
                SqlDataAdapter adapter = new(SqlQuerys.Select(month, year, tupeOfSort), ConnectionStrings.connectionString);
                adapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                    list.Add(new ModelToTable
                    {
                        Name = dt.Rows[i]["Name"].ToString().Trim(),
                        Model = dt.Rows[i]["Model"].ToString().Trim(),
                        Money = Convert.ToDouble(dt.Rows[i]["Price"]),
                        DateStart = GetDate(dt.Rows[i]["DateStart"], month, year),
                        DateEnd = GetDate(dt.Rows[i]["DateEnd"], month, year)
                    });
            }
            catch (SqlException)
            {
                Console.WriteLine("Ошибка чтения из базы");
            }
            
            return list;
        }
        private DateTime GetDate(object dataRow, int month, int year)
        {
            DateTime minDate = new(year, month, 1);
            DateTime maxDate = minDate.AddMonths(1).AddHours(-1);
            if (dataRow == DBNull.Value)
            {
                return maxDate;
            }
            var date = (DateTime)dataRow;

            if (date < minDate)
                return minDate;

            if (date > maxDate)
                return maxDate;

            return date;
        }
    }
}
