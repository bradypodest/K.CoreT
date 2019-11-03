using K.Core.IServices.BASE;
using K.Core.Model.Models;
using System.Threading.Tasks;

namespace K.Core.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IRoleServices :IBaseServices<Role>
	{
        Task<Role> SaveRole(string roleName);
        Task<string> GetRoleNameByRid(int rid);

    }
}
