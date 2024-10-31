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
        private readonly IRepository<SettingEntity> _settingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenanceState()
        {
            var maintenanceState = _settingRepository.GetById(1).MaintenanceMode;

            return maintenanceState;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenanceMode = !setting.MaintenanceMode; 
            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Bakım durumu güncellenirken bir hata ile karşılaşıldı");
            }




        }
    }
}
