using K.Core.IRepository.Base;
using K.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K.Core.IRepository
{	
	/// <summary>
	/// IRoleModulePermissionRepository
	/// </summary>	
	public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>//类名
    {
        Task<List<RoleModulePermission>> WithChildrenModel();
        Task<List<TestMuchTableResult>> QueryMuchTable();
    }
}
