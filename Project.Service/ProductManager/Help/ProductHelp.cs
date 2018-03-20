using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.ProductManager;
using Project.Repository.ProductManager;

namespace Project.Service.ProductManager.Help
{
    public class ProductHelp
    {

        #region 构造函数

        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly SystemCategoryRepository _systemCategoryRepository;
        private static readonly ProductHelp Instance = new ProductHelp();

        public ProductHelp()
        {
            this._productRepository = new ProductRepository();
            _productCategoryRepository=new ProductCategoryRepository();
            _systemCategoryRepository=new SystemCategoryRepository();
        }

        public static ProductHelp GetInstance()
        {
            return Instance;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void CombineProductInfo(ProductEntity entity)
        {
            if (entity.ProductCategoryId>0)
            {
              var temp =  _productCategoryRepository.GetById(entity.ProductCategoryId);
                if (temp != null)
                {
                    entity.ProductCategoryName = temp.ProductCategoryName;
                }
            }

            if (entity.SystemCategoryId > 0)
            {
                var temp = _systemCategoryRepository.GetById(entity.SystemCategoryId);
                if (temp != null)
                {
                    entity.SystemCategoryName = temp.SystemCategoryName;
                }
            }
        }

    }
}
