﻿
 /***************************************************************************
 *       功能：     HRAttendance映射类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤记录
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class AttendanceMap : BaseMap<AttendanceEntity,int>
    {
        public AttendanceMap():base("HR_Attendance")
        {
            this.MapPkidDefault<AttendanceEntity,int>();
 
            Map(p => p.AttendanceUploadRecordId);    
            Map(p => p.EmployeeCode);
            Map(p => p.EmployeeName);   
            Map(p => p.DepartmentCode);    
            Map(p => p.DepartmentName);    
            Map(p => p.State);    
            Map(p => p.Date);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);     
            Map(p => p.CreationTime);
            Map(p => p.LastModificationTime);
            Map(p => p.LastModifierUserCode);   
            Map(p => p.IsDelete);    
        }
    }
}

    
 

