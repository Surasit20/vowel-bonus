using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Domain.Interfaces
{
    public interface IVowelBonusScoreHistoryRepository
    {
        Task<VowelBonusScoreHistory> GetByIdAsync(int id);

        Task<int> GetTotalPointByUserIdAsync(int id);

        Task<IEnumerable<VowelBonusScoreHistory>> GetAllAsync();

        Task<IEnumerable<VowelBonusScoreHistory>> GetByTaskAsync(int userId, int task);

        Task AddAsync(VowelBonusScoreHistory history);

        Task UpdateAsync(VowelBonusScoreHistory history);

        Task DeleteAsync(int id);
    }
}
