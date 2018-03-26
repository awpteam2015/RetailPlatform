
/***************************************************************************
*       功能：     CNMPageContentCategory业务处理层
*       作者：     李伟伟
*       日期：     2018/3/17
*       描述：     内容分类
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.ContentManager;
using Project.Repository.ContentManager;

namespace Project.Service.ContentManager
{
    public class PageContentCategoryService
    {

        #region 构造函数
        private readonly PageContentCategoryRepository _pageContentCategoryRepository;
        private static readonly PageContentCategoryService Instance = new PageContentCategoryService();

        public PageContentCategoryService()
        {
            this._pageContentCategoryRepository = new PageContentCategoryRepository();
        }

        public static PageContentCategoryService GetInstance()
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
        public System.Int32 Add(PageContentCategoryEntity entity)
        {
            return _pageContentCategoryRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _pageContentCategoryRepository.GetById(pkId);
                entity.IsDeleted = 1;
                _pageContentCategoryRepository.Update(entity);
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
        public bool Delete(PageContentCategoryEntity entity)
        {
            try
            {
                _pageContentCategoryRepository.Delete(entity);
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
        public bool Update(PageContentCategoryEntity entity)
        {
            try
            {
                _pageContentCategoryRepository.Merge(entity);
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
        public PageContentCategoryEntity GetModelByPk(System.Int32 pkId)
        {
            return _pageContentCategoryRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【内容分类】和总【内容分类】数</returns>
        public System.Tuple<IList<PageContentCategoryEntity>, int> Search(PageContentCategoryEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<PageContentCategoryEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.PageContentCategoryName))
            //  expr = expr.And(p => p.PageContentCategoryName == where.PageContentCategoryName);
            // if (!string.IsNullOrEmpty(where.ParentId))
            //  expr = expr.And(p => p.ParentId == where.ParentId);
            // if (!string.IsNullOrEmpty(where.Rank))
            //  expr = expr.And(p => p.Rank == where.Rank);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.Route))
            //  expr = expr.And(p => p.Route == where.Route);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            #endregion
            var list = _pageContentCategoryRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _pageContentCategoryRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<PageContentCategoryEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<PageContentCategoryEntity> GetList(PageContentCategoryEntity where)
        {
            var expr = PredicateBuilder.True<PageContentCategoryEntity>();
            expr = expr.And(p =>  p.IsDeleted != 1);
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.PageContentCategoryName))
                expr = expr.And(p => p.PageContentCategoryName == where.PageContentCategoryName);
            // if (!string.IsNullOrEmpty(where.ParentId))
            //  expr = expr.And(p => p.ParentId == where.ParentId);
            // if (!string.IsNullOrEmpty(where.Rank))
            //  expr = expr.And(p => p.Rank == where.Rank);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.Route))
            //  expr = expr.And(p => p.Route == where.Route);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleterUserCode))
            //  expr = expr.And(p => p.DeleterUserCode == where.DeleterUserCode);
            // if (!string.IsNullOrEmpty(where.DeletionTime))
            //  expr = expr.And(p => p.DeletionTime == where.DeletionTime);
            #endregion
            var list = _pageContentCategoryRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        /// <summary>
        /// 获取顶级内容分类
        /// </summary>
        /// <returns></returns>
        public IList<PageContentCategoryEntity> GetTopPageContentCategoryList()
        {
            return _pageContentCategoryRepository.Query().Where(p => p.ParentId == 0&&p.IsDeleted!=1).ToList();
        }

        #endregion
    }
}




