using MyWealth.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenence(); //Used to put into maintenance mode
        bool GetMaintenanceState(); // To find out the situation
    }
}
