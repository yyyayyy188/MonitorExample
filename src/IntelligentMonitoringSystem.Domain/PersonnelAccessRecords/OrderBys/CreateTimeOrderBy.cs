using IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;

/// <summary>
///     按创建时间排序
/// </summary>
public class CreateTimeOrderBy : OrderBy<PersonnelAccessRecord, dynamic>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateTimeOrderBy" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="isDescending">是否降序</param>
    public CreateTimeOrderBy(bool isDescending = true) : base(x => x.CreateTime, isDescending)
    {
    }
}