using Entities;
using Entities.Models;
using Repository.Interface;

namespace Repository.Implement
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
