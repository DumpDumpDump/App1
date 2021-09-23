using System.Collections.Generic;
using System.Threading.Tasks;

using EntitiesAndModels;

namespace Writer
{
    public interface IWriterService
    {
        Task<PostItemMain> PostItemMain(PostItemMain data);
        Task<IEnumerable<GetItems>> PostItemDetail(PostItemDetail data);
        Task<PatchItemMain> PatchItemMain(PatchItemMain data);
        Task<IEnumerable<GetItems>> PatchItemDetail(PatchItemDetail data);
        Task DeleteItemMain(string ID);
        Task DeleteItemDetail(string ID);
    }
}
