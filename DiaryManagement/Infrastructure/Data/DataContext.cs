using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Context class to access database
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region DBSets

        // Your DbSets..

        #endregion
    }
}
