using school_api.Data.Models;

namespace school_api.Repoistories.Base
{
    public interface IAccountRepoistory
    {
        Task<bool> IsEmailUniqueAsync( string email );
        Task<User> GetUserByEmailAsync( string email );
    }
}
