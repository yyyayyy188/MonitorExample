using MiniExcelLibs.Attributes;

namespace IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

/// <summary>
///     导出出入记录
/// </summary>
public class ExportPersonnelAccessRecordDto
{
    /// <summary>
    ///     Id
    /// </summary>
    [ExcelColumn(Name = "出入记录编号", Index = 0, Width = 20)]
    public int PersonnelAccessRecordId { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [ExcelColumn(Name = "姓名", Index = 1, Width = 20)]
    public string? Name { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    [ExcelColumn(Name = "年龄", Index = 2, Width = 20)]
    public short Age { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    [ExcelColumn(Name = "性别", Index = 3, Width = 20)]
    public string Gender { get; set; }

    /// <summary>
    ///     出入时间
    /// </summary>
    [ExcelColumn(Name = "抓拍时间", Index = 4, Width = 30)]
    public string AccessTime { get; set; }

    /// <summary>
    ///     出入状态
    /// </summary>
    [ExcelColumn(Name = "出入状态", Index = 5, Width = 20)]
    public string AccessStatus { get; set; }

    /// <summary>
    ///     出入类型
    /// </summary>
    [ExcelColumn(Name = "出入类型", Index = 6, Width = 20)]
    public string AccessType { get; set; }

    /// <summary>
    ///     请假状态
    /// </summary>
    [ExcelColumn(Name = "请假状态", Index = 7, Width = 20)]
    public string LeaveStatus { get; set; }
}