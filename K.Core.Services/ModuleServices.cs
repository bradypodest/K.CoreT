using K.Core.Services.BASE;
using K.Core.Model.Models;
using K.Core.IRepository;
using K.Core.IServices;

namespace K.Core.Services
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
	public class ModuleServices : BaseServices<Module>, IModuleServices
    {
	
        IModuleRepository _dal;
        public ModuleServices(IModuleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
       
    }
}
