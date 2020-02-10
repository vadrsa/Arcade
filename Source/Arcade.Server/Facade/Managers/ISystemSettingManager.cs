using SharedEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface ISystemSettingManager
    {
        Task SetSetting(SystemSettingDto settingDto);
        Task<SystemSettingDto> GetSetting(int id);
        Task<List<SystemSettingDto>> GetAllSettings();
    }
}
