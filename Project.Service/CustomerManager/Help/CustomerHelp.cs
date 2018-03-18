using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Repository.SystemSetManager;

namespace Project.Service.CustomerManager.Help
{
   public class CustomerHelp
    {
        private static readonly CustomerHelp Instance = new CustomerHelp();
        private readonly ProvinceRepository _provinceRepository;
        private readonly CityRepository _cityRepository;
        private readonly AreaRepository _areaRepository;

        private CustomerHelp()
        {
            _provinceRepository = new ProvinceRepository();
            this._cityRepository = new CityRepository();
            this._areaRepository = new AreaRepository();
        }

        public static CustomerHelp GetInstance()
        {
            return Instance;
        }

       public string CombineCustomerAddress(string provinceId,string cityId,string areaId,string Address)
       {
           var addressFull = "";
           if (!string.IsNullOrEmpty(provinceId))
           {
               addressFull += _provinceRepository.Query().Where(p => p.ProvinceId == provinceId).FirstOrDefault().Province;
           }

            if (!string.IsNullOrEmpty(cityId))
            {
                addressFull += _cityRepository.Query().Where(p => p.CityId == cityId).FirstOrDefault().City;
            }


            if (!string.IsNullOrEmpty(areaId))
            {
                addressFull += _areaRepository.Query().Where(p => p.AreaId == areaId).FirstOrDefault().Area;
            }

           return addressFull+"    "+Address;
       }
    }
}
