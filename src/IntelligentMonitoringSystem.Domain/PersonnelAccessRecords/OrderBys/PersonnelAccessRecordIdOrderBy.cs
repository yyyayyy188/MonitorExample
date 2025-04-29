using IntelligentMonitoringSystem.Domain.Shared.Linq.OrderBy;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.OrderBys;

/// <summary>
///     排序字段
/// </summary>
public class PersonnelAccessRecordIdOrderBy : OrderBy<PersonnelAccessRecord, dynamic>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonnelAccessRecordIdOrderBy" /> class.
    ///     构造函数
    /// </summary>
    /// <param name="isDescending">是否降序</param>
    public PersonnelAccessRecordIdOrderBy(bool isDescending = true) : base(x => x.Id, isDescending)
    {
    }
}