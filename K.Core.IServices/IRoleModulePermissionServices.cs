using K.Core.IServices.BASE;
using K.Core.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace K.Core.IServices
{	
	/// <summary>
	/// RoleModulePermissionServices
	/// </summary>	
    public interface IRoleModulePermissionServices :IBaseServices<RoleModulePermission>
	{

        Task<List<RoleModulePermission>> GetRoleModule();
        Task<List<RoleModulePermission>> TestModelWithChildren();
        Task<List<TestMuchTableResult>> QueryMuchTable();
    }
}
