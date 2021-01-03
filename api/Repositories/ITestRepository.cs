using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories
{
    public interface ITestRepository
    {
        Task<List<string>> GetStrings();
    }
}