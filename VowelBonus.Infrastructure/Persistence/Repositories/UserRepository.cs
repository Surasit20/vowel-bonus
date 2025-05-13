using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VowelBonus.Shared.Common.Models;
using VowelBonus.Shared.DTOs;

namespace VowelBonus.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(f=> f.UserId == id && 
                                                               f.IsDelete == false && 
                                                               f.IsActive == true);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.Where(f => f.IsDelete == false &&
                                                  f.IsActive == true).ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            var isExistsUser = await _context.User.AnyAsync(f => f.UserName == user.UserName &&
                                                                 f.IsDelete == false &&
                                                                 f.IsActive == true);

            if(isExistsUser == false)
            {
                await _context.User.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new Exception(BaseConst.ERROR_EXIST_USER);
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userExist = await _context.User.FindAsync(user.UserId);
            if (userExist != null)
            {
                userExist.UserName = user.UserName;
                _context.User.Update(userExist);
                await _context.SaveChangesAsync();
                return userExist;
            }
            else
            {
                throw new Exception(BaseConst.NOT_FOUND);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                user.IsDelete = true;
                user.IsActive = false;
                _context.User.Update(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(BaseConst.NOT_FOUND);
            }
        }

        public async Task<bool> ExistsAsync(string userName)
        {
            return await _context.User.AnyAsync(f => f.UserName == userName);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.User.Include(i => i.VowelBonusScoreHistories
                                       .Where(f => f.IsActive == true && 
                                                   f.IsDelete == false).OrderByDescending(v => v.CreatedDate).Take(BaseConst.DEFAULT_TASK)) //<--- รอทำ Paging
                                      .FirstOrDefaultAsync(f => f.UserName == userName && 
                                                                f.IsDelete == false && 
                                                                f.IsActive == true);
        }
    }
}
