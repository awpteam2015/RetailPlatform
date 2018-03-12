
 /***************************************************************************
 *       功能：     PRMSystemCategoryAttribute映射类
 *       作者：     李伟伟
 *       日期：     2017/6/30
 *       描述：     系统分类对应属性
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.ProductManager;

namespace  Project.Map.ProductManager
{
    public class SystemCategoryAttributeMap : BaseMap<SystemCategoryAttributeEntity,int>
    {
        public SystemCategoryAttributeMap():base("PRM_SystemCategoryAttribute")
        {
            this.MapPkidDefault<SystemCategoryAttributeEntity,int>();
 
            Map(p => p.AttributeId);    
            Map(p => p.SystemCategoryId);    
            Map(p => p.IsMust);
            Map(p => p.Sort);


            References(p => p.AttributeEntity)
                   .LazyLoad()
                  .Not.Insert()
                  .Not.Update()
                  .NotFound.Ignore()
                  .Column("AttributeId");
        }
    }
}

    
 

