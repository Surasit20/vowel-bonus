using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VowelBonus.Shared.DTOs;

namespace VowelBonus.Domain.Interfaces
{
    public interface IVowelBonusScoreHistoryRepository
    {
        Task<VowelBonusScoreHistory> GetByIdAsync(int id);

        Task<int> GetTotalPointByUserIdAsync(int id);

        Task<int> GetCountByUserIdAsync(int id);
        Task<int> GetCountByFilterAsync(VowelBonusScoreHistoryFilterDto filterDto);

        Task<IEnumerable<VowelBonusScoreHistory>> GetAllAsync();

        Task<IEnumerable<VowelBonusScoreHistory>> GetByTaskAsync(int userId, int task);

        Task<IEnumerable<VowelBonusScoreHistory>> GetByFilterAsync(VowelBonusScoreHistoryFilterDto filterDto);

        Task AddAsync(VowelBonusScoreHistory history);

        Task UpdateAsync(VowelBonusScoreHistory history);

        Task DeleteAsync(int id);
    }
}
