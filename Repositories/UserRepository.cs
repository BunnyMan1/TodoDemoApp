using Todo.Models;
using Dapper;
using Todo.Utilities;

namespace Todo.Repositories;

public interface IUserRepository
{
}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }
}