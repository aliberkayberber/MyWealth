using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IRepository<SettingEntity> _settingRepository; // for maintenance operations
        private readonly IUnitOfWork _unitOfWork; // for database operations

        // we do dependency injection.
        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        // To find out the situation
        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetById(1).MaintenanceMode;

            return maintenanceState;
        }

        //Used to put into maintenance mode
        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenanceMode = !setting.MaintenanceMode; 
            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();  // Setting are saved to database
            }
            catch (Exception)
            {
                throw new Exception("An error was encountered while updating the maintenance status");
            }

        }
    }
}
