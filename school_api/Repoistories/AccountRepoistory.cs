using Microsoft.EntityFrameworkCore;
using school_api.Data;
using school_api.Data.Models;
using school_api.Repoistories.Base;

namespace school_api.Repoistories
{
    public class AccountRepoistory : IAccountRepoistory
    {
        private readonly AppDbContext _context;
        public AccountRepoistory( AppDbContext context )
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync( string email )
        {
            var user = await _context.Users.FirstOrDefaultAsync( x => x.Email == email );
            if (user == null)
                throw new KeyNotFoundException( "User Not Found!" );

            return user;
        }

        public async Task<bool> IsEmailUniqueAsync( string email )
        {
            var isUnique = await _context.Users.AllAsync( u => u.Email != email );
            return isUnique;
        }
    }
}
