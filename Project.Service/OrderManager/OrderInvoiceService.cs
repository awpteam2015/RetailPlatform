
 /***************************************************************************
 *       功能：     OMOrderInvoice业务处理层
 *       作者：     李伟伟
 *       日期：     2018/3/17
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.OrderManager;
using Project.Repository.OrderManager;

namespace Project.Service.OrderManager
{
    public class OrderInvoiceService
    {
       
       #region 构造函数
        private readonly OrderInvoiceRepository  _orderInvoiceRepository;
            private static readonly OrderInvoiceService Instance = new OrderInvoiceService();

        public OrderInvoiceService()
        {
           this._orderInvoiceRepository =new OrderInvoiceRepository();
        }
        
         public static  OrderInvoiceService GetInstance()
        {
            return Instance;
        }
        #endregion


        #region 基础方法 
         /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public System.Int32 Add(OrderInvoiceEntity entity)
        {
            return _orderInvoiceRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _orderInvoiceRepository.GetById(pkId);
            _orderInvoiceRepository.Delete(entity);
             return true;
        }
        catch
        {
         return false;
        }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(OrderInvoiceEntity entity)
        {
         try
            {
            _orderInvoiceRepository.Delete(entity);
             return true;
        }
        catch
        {
         return false;
        }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(OrderInvoiceEntity entity)
        {
          try
            {
            _orderInvoiceRepository.Update(entity);
         return true;
        }
        catch
        {
         return false;
        }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public OrderInvoiceEntity GetModelByPk(System.Int32 pkId)
        {
            return _orderInvoiceRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<OrderInvoiceEntity>, int> Search(OrderInvoiceEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<OrderInvoiceEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.InvoiceTitle))
              //  expr = expr.And(p => p.InvoiceTitle == where.InvoiceTitle);
              // if (!string.IsNullOrEmpty(where.InvoiceContent))
              //  expr = expr.And(p => p.InvoiceContent == where.InvoiceContent);
              // if (!string.IsNullOrEmpty(where.InvoiceCompany))
              //  expr = expr.And(p => p.InvoiceCompany == where.InvoiceCompany);
              // if (!string.IsNullOrEmpty(where.InvoiceNo))
              //  expr = expr.And(p => p.InvoiceNo == where.InvoiceNo);
              // if (!string.IsNullOrEmpty(where.Money))
              //  expr = expr.And(p => p.Money == where.Money);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
 #endregion
            var list = _orderInvoiceRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _orderInvoiceRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<OrderInvoiceEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<OrderInvoiceEntity> GetList(OrderInvoiceEntity where)
        {
               var expr = PredicateBuilder.True<OrderInvoiceEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.OrderNo))
              //  expr = expr.And(p => p.OrderNo == where.OrderNo);
              // if (!string.IsNullOrEmpty(where.InvoiceTitle))
              //  expr = expr.And(p => p.InvoiceTitle == where.InvoiceTitle);
              // if (!string.IsNullOrEmpty(where.InvoiceContent))
              //  expr = expr.And(p => p.InvoiceContent == where.InvoiceContent);
              // if (!string.IsNullOrEmpty(where.InvoiceCompany))
              //  expr = expr.And(p => p.InvoiceCompany == where.InvoiceCompany);
              // if (!string.IsNullOrEmpty(where.InvoiceNo))
              //  expr = expr.And(p => p.InvoiceNo == where.InvoiceNo);
              // if (!string.IsNullOrEmpty(where.Money))
              //  expr = expr.And(p => p.Money == where.Money);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
 #endregion
            var list = _orderInvoiceRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 

