namespace TestForLines.SQL
{
    internal class SqlQuerys
    {
        public static string Select(int month, int year, string tupeOfSort) =>
            $"select Cars.Model, WorkOnCars.Price, Masters.Name, WorkOnCars.DateStart, WorkOnCars.DateEnd from Cars inner join WorkOnCars on Cars.ID = WorkOnCars.CraID inner join Masters on Masters.ID = WorkOnCars.MasterID where (MONTH(DateEnd) = {month} or MONTH(DateStart) = {month}) and (YEAR(DateStart) = {year} or YEAR(DateEnd) = {year}) order by {tupeOfSort}";
    }
}