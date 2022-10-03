namespace PgSqlMigrator_Core
{
    /// <summary>
    /// Список сохранённых параметров
    /// </summary>
    public class ProgramData
    {
        public string inAddress { get; set; }
        public string inLogin { get; set; }
        public string inPass { get; set; }
        public string inDB { get; set; }
        public string outAddress { get; set; }
        public string outLogin { get; set; }
        public string outPass { get; set; }
        public string outDB { get; set; }
        public string key { get; set; }
    }
}
