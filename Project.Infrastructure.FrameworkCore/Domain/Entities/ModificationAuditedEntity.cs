using System;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Auditing.Abstract;

namespace Project.Infrastructure.FrameworkCore.Domain.Entities
{
    /// <summary>
    /// Update
    /// </summary>
     [Serializable]
    public abstract class ModificationAuditedEntity : ModificationAuditedEntity<int>
    {

    }

 

}
