using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Managers
{
    public interface ITestManager
    {
        Task<List<string>> DoSomething();
        Task WriteNameToQueue(string name);
    }
}
