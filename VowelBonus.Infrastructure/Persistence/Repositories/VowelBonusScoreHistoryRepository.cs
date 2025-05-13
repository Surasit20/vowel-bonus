using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Domain.Persistence.Repositories
{
    public class VowelBonusScoreHistoryRepository : IVowelBonusScoreHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public VowelBonusScoreHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VowelBonusScoreHistory> GetByIdAsync(int id)
        {
            return await _context.VowelBonusScoreHistory.FindAsync(id);
        }

        public async Task<IEnumerable<VowelBonusScoreHistory>> GetAllAsync()
        {
            return await _context.VowelBonusScoreHistory.ToListAsync();
        }

        public async Task AddAsync(VowelBonusScoreHistory history)
        {
            await _context.VowelBonusScoreHistory.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VowelBonusScoreHistory history)
        {
            _context.VowelBonusScoreHistory.Update(history);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var history = await _context.VowelBonusScoreHistory.FindAsync(id);
            if (history != null)
            {
                _context.VowelBonusScoreHistory.Remove(history);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalPointByUserIdAsync(int userId)
        {
            return  await _context.VowelBonusScoreHistory.Where(f=> f.UserId == userId && 
                                                                    f.IsDelete == false &&
                                                                    f.IsActive == true)
                                                         .SumAsync(f => f.Point);
        }

        public async Task<IEnumerable<VowelBonusScoreHistory>> GetByTaskAsync(int userId, int task)
        {
            return await _context.VowelBonusScoreHistory.Where(f => f.UserId == userId &&
                                                                    f.IsDelete == false &&
                                                                    f.IsActive == true).OrderByDescending(o=>o.CreatedDate).Take(task).ToListAsync();
        }
    }
}
