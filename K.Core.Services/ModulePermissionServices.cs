using K.Core.Services.BASE;
using K.Core.Model.Models;
using K.Core.IRepository;
using K.Core.IServices;

namespace K.Core.Services
{	
	/// <summary>
	/// ModulePermissionServices
	/// </summary>	
	public class ModulePermissionServices : BaseServices<ModulePermission>, IModulePermissionServices
    {
	
        IModulePermissionRepository _dal;
        public ModulePermissionServices(IModulePermissionRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
       
    }
}
