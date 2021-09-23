using System.Collections.Generic;
using System.Threading.Tasks;

using EntitiesAndModels;

namespace Reader
{
    public interface IReaderService
    {
        Task<IEnumerable<GetItems>> GetAllItems();
        Task<IEnumerable<GetItems>> GetItemsByNoKK(string filter);
        Task<IEnumerable<GetItems>> GetItemsByNamaKK(string filter);
        Task<IEnumerable<GetItems>> GetItemsByNamaLengkap(string filter);
        Task<IEnumerable<GetItems>> GetItemsByNIK(string filter);
    }
}
