using System.Threading.Tasks;

namespace BuryChoice.Services
{
    public interface IJsonFileReader
    {
        Task<T?> ReadJsonFileAsync<T>(string relativePath, bool fromWebRoot = false);
    }
}
