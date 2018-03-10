
 /***************************************************************************
 *       功能：     PRMSystemCategory映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SystemCategoryMap : BaseMap<SystemCategoryEntity,int>
    {
        public SystemCategoryMap():base("PRM_SystemCategory")
        {
            this.MapPkidDefault<SystemCategoryEntity,int>();
 
            Map(p => p.SystemCategoryName);    
            Map(p => p.Sort);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
            Map(p => p.DeleterUserCode);    
            Map(p => p.DeletionTime);

            HasMany(p => p.SystemCategoryAttributeList)
      .AsSet()
      .LazyLoad()
      .Cascade.All().Inverse()
      .KeyColumn("SystemCategoryId");


            HasMany(p => p.SystemCategoryBrandList)
      .AsSet()
      .LazyLoad()
      .Cascade.All().Inverse()
      .KeyColumn("SystemCategoryId");


            HasMany(p => p.SystemCategorySpecList)
      .AsSet()
      .LazyLoad()
      .Cascade.All().Inverse()
      .KeyColumn("SystemCategoryId");

        }
    }
}

    
 

