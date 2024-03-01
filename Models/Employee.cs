namespace CompanyData.Models
{
    public class Employee
    {
        public int? Empno { get; set; }
        public string? Ename { get; set; }
        public string Job { get; set; }
        public int? Mgr { get; set; }
        public DateTime Hiredate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Commision { get; set; }
        public int Deptno { get; set; }

        public override string ToString()
        {
            return $"{Empno} ->- {Ename} ";
        }
    }
}
