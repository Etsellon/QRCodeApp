using Microsoft.EntityFrameworkCore;
using QRCodeApp.Core.Abstractions.Interfaces;
using QRCodeApp.Core.Models;
using QRCodeApp.DataAccess.Mappers;

namespace QRCodeApp.DataAccess.Repositories
{
    public class QrInfosRepository : IQrInfosRepository
    {
        private readonly QrInfoDbContext _context;

        public QrInfosRepository(QrInfoDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(QrInfo qrcode)
        {
            var qrcodeEntity = QrInfoMapper.ToEntity(qrcode);

            await _context.QrCodes.AddAsync(qrcodeEntity);
            await _context.SaveChangesAsync();

            return qrcodeEntity.Id;
        }

        public async Task<List<QrInfo>> GetAll()
        {
            var qrcodeEntity = await _context.QrCodes
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            var qrcodes = qrcodeEntity
                .Select(x => QrInfoMapper.ToDomain(x))
                .ToList();

            return qrcodes;
        }

        public async Task<QrInfo?> GetById(Guid id)
        {
            var qrcodeEntity = await _context.QrCodes
                .FindAsync(id);

            if (qrcodeEntity == null)
                return null;

            return QrInfoMapper.ToDomain(qrcodeEntity);
        }
        public async Task<List<QrInfo>> GetByPage(int page, int pageSize)
        {
            var qrcodeEntity = await _context.QrCodes
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var qrcodes = qrcodeEntity
                .Select(x => QrInfoMapper.ToDomain(x))
                .ToList();

            return qrcodes;
        }
    }
}
