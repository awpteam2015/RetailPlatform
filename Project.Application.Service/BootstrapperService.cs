using AutoMapper;
using Project.Application.Service.OrderManager.Request;
using Project.Infrastructure.FrameworkCore.AutoMapper;
using Project.Model.HRManager;
using Project.Model.OrderManager;
using Project.Model.PermissionManager;
using Project.Model.ProductManager;
using Project.Model.RiverManager;
using Project.Service.PermissionManager.DTO;

namespace Project.Application.Service
{
    public static class BootstrapperApplicationService
    {
        public static void Init()
        {
            InitAutoMapper();
        }

        private static void InitAutoMapper()
        {

            Mapper.CreateMap<AddOrderRequest, OrderMainEntity>().IgnoreAllNull();
           
        }

    }
}
