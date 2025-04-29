using IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;

/// <summary>
///     创建时间排序
/// </summary>
public class AbnormalPersonnelAccessRecordCreateTimeOrderBy : OrderBy<AbnormalPersonnelAccessRecord, dynamic>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AbnormalPersonnelAccessRecordCreateTimeOrderBy" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="isDescending">是否降序</param>
    public AbnormalPersonnelAccessRecordCreateTimeOrderBy(bool isDescending = true) : base(x => x.CreateTime, isDescending)
    {
    }
}