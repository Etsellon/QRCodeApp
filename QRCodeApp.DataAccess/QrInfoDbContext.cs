using Microsoft.EntityFrameworkCore;
using QRCodeApp.DataAccess.Entities;

namespace QRCodeApp.DataAccess
{
    public class QrInfoDbContext : DbContext
    {
        public DbSet<QrInfoEntity> QrCodes { get; set; }

        public QrInfoDbContext(DbContextOptions<QrInfoDbContext> options) : base(options)
        {
            QrCodes = Set<QrInfoEntity>();
        }
    }
}
