using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VowelBonus.Shared.DTOs;
using System.Linq.Dynamic.Core;
using VowelBonus.Domain.Entities;

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
            return await _context.VowelBonusScoreHistory.Where(f => f.UserId == userId &&
                                                                    f.IsDelete == false &&
                                                                    f.IsActive == true)
                                                         .SumAsync(f => f.Point);
        }

        public async Task<IEnumerable<VowelBonusScoreHistory>> GetByTaskAsync(int userId, int task)
        {
            return await _context.VowelBonusScoreHistory.Where(f => f.UserId == userId &&
                                                                    f.IsDelete == false &&
                                                                    f.IsActive == true).OrderByDescending(o => o.CreatedDate).Take(task).ToListAsync();
        }

        public async Task<IEnumerable<VowelBonusScoreHistory>> GetByFilterAsync(VowelBonusScoreHistoryFilterDto filterDto)
        {
            var query = _context.VowelBonusScoreHistory.Where(f => f.UserId == filterDto.UserId &&
                                                                    (filterDto.StartWord == null || string.Compare(f.Word, filterDto.StartWord) >= 0) &&
                                                                    (filterDto.EndWord == null || string.Compare(filterDto.EndWord, f.Word) >= 0) &&
                                                                    (filterDto.StartPoint == null || filterDto.StartPoint <= f.Point) &&
                                                                    (filterDto.EndPoint == null || filterDto.EndPoint >= f.Point) &&
                                                                    f.IsDelete == false &&
                                                                    f.IsActive == true);

            if (!string.IsNullOrEmpty(filterDto.SortBy))
            {
                var sortDirection = filterDto.SortDirection?.ToLower() == "desc" ? "descending" : "ascending";
                query = query.OrderBy($"{filterDto.SortBy} {sortDirection}");
            }
            else
            {
                query = query.OrderByDescending(o => o.CreatedDate);
            }

            var result = await query
                .Skip(filterDto.Skip)
                .Take(filterDto.PageSize)
                .ToListAsync();

            return result;
        }

        public async Task<int> GetCountByUserIdAsync(int userId)
        {
            return await _context.VowelBonusScoreHistory.CountAsync(f => f.UserId == userId &&
                                                                         f.IsDelete == false &&
                                                                         f.IsActive == true);
        }

        public async Task<int> GetCountByFilterAsync(VowelBonusScoreHistoryFilterDto filterDto)
        {
            return await _context.VowelBonusScoreHistory.CountAsync(f => f.UserId == filterDto.UserId &&
                                                                         (filterDto.StartWord == null || string.Compare(f.Word, filterDto.StartWord) >= 0) &&
                                                                         (filterDto.EndWord == null || string.Compare(filterDto.EndWord, f.Word) >= 0) &&
                                                                         (filterDto.StartPoint == null || filterDto.StartPoint <= f.Point) &&
                                                                         (filterDto.EndPoint == null || filterDto.EndPoint >= f.Point) &&
                                                                         f.IsDelete == false &&
                                                                         f.IsActive == true);
        }
    }
}
