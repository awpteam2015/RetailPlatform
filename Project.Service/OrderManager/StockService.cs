using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Impl;
using NHibernate.Util;
using Project.Model.OrderManager;
using Project.Repository.ProductManager;

namespace Project.Service.OrderManager
{
    public class StockService
    {

        private readonly GoodsRepository _goodsRepository;

        public StockService()
        {
            _goodsRepository = new GoodsRepository();
        }

        /// <summary>
        /// 库存检查
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Tuple<bool, string> StockCheck(OrderMainEntity orderInfo)
        {
            var flag = true;
            orderInfo.OrderMainDetailEntityList.ForEach(p =>
            {
                var goodsInfo = _goodsRepository.GetById(p.GoodsId);
                if (goodsInfo.GoodsStock < p.Num)
                {
                    flag = false;
                }
            });
            return new Tuple<bool, string>(false, "库存不足");
        }
    }
}
