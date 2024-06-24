using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Presentation.FrameWork;

namespace Mc2.CrudTest.Presentation.DataAccessServiceContract
{
    public interface IBaseRepository<TKey , TModel , TAddModel , TUpdateModel>
    {
        Task<OperationResult> Delete(TKey key);
        Task<OperationResult> Add(TAddModel model);
        Task<OperationResult> Update(TUpdateModel model);
        Task<TModel> GetModel(TKey key);
        List<TModel> GetAll();
    }
}
