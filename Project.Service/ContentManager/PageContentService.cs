
/***************************************************************************
*       功能：     CNMPageContent业务处理层
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ContentManager;
using Project.Repository.ContentManager;

namespace Project.Service.ContentManager
{
    public class PageContentService
    {

        #region 构造函数
        private readonly PageContentRepository _pageContentRepository;
        private static readonly PageContentService Instance = new PageContentService();

        public PageContentService()
        {
            this._pageContentRepository = new PageContentRepository();
        }

        public static PageContentService GetInstance()
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
        public System.Int32 Add(PageContentEntity entity)
        {
            return _pageContentRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _pageContentRepository.GetById(pkId);
                _pageContentRepository.Delete(entity);
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
        public bool Delete(PageContentEntity entity)
        {
            try
            {
                _pageContentRepository.Delete(entity);
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
        public bool Update(PageContentEntity entity)
        {
            try
            {
                _pageContentRepository.Merge(entity);
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
        public PageContentEntity GetModelByPk(System.Int32 pkId)
        {
            return _pageContentRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<PageContentEntity>, int> Search(PageContentEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<PageContentEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Title1))
                expr = expr.And(p => p.Title1.IsLike(where.Title1) || p.Title2.IsLike(where.Title1) || p.Title3.IsLike(where.Title1));
            // if (!string.IsNullOrEmpty(where.Title2))
            //  expr = expr.And(p => p.Title2 == where.Title2);
            // if (!string.IsNullOrEmpty(where.Title3))
            //  expr = expr.And(p => p.Title3 == where.Title3);
            // if (!string.IsNullOrEmpty(where.Description1))
            //  expr = expr.And(p => p.Description1 == where.Description1);
            // if (!string.IsNullOrEmpty(where.Description2))
            //  expr = expr.And(p => p.Description2 == where.Description2);
            // if (!string.IsNullOrEmpty(where.Description3))
            //  expr = expr.And(p => p.Description3 == where.Description3);
            // if (!string.IsNullOrEmpty(where.ImageUrl1))
            //  expr = expr.And(p => p.ImageUrl1 == where.ImageUrl1);
            // if (!string.IsNullOrEmpty(where.ImageUrl2))
            //  expr = expr.And(p => p.ImageUrl2 == where.ImageUrl2);
            // if (!string.IsNullOrEmpty(where.ImageUrl3))
            //  expr = expr.And(p => p.ImageUrl3 == where.ImageUrl3);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            #endregion
            var list = _pageContentRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _pageContentRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<PageContentEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<PageContentEntity> GetList(PageContentEntity where)
        {
            var expr = PredicateBuilder.True<PageContentEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.Title1))
            //  expr = expr.And(p => p.Title1 == where.Title1);
            // if (!string.IsNullOrEmpty(where.Title2))
            //  expr = expr.And(p => p.Title2 == where.Title2);
            // if (!string.IsNullOrEmpty(where.Title3))
            //  expr = expr.And(p => p.Title3 == where.Title3);
            // if (!string.IsNullOrEmpty(where.Description1))
            //  expr = expr.And(p => p.Description1 == where.Description1);
            // if (!string.IsNullOrEmpty(where.Description2))
            //  expr = expr.And(p => p.Description2 == where.Description2);
            // if (!string.IsNullOrEmpty(where.Description3))
            //  expr = expr.And(p => p.Description3 == where.Description3);
            // if (!string.IsNullOrEmpty(where.ImageUrl1))
            //  expr = expr.And(p => p.ImageUrl1 == where.ImageUrl1);
            // if (!string.IsNullOrEmpty(where.ImageUrl2))
            //  expr = expr.And(p => p.ImageUrl2 == where.ImageUrl2);
            // if (!string.IsNullOrEmpty(where.ImageUrl3))
            //  expr = expr.And(p => p.ImageUrl3 == where.ImageUrl3);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            #endregion
            var list = _pageContentRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}




