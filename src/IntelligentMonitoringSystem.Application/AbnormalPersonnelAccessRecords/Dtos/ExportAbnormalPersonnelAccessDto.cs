using MiniExcelLibs.Attributes;

namespace IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

/// <summary>
///     导出异常人员进出记录
/// </summary>
public class ExportAbnormalPersonnelAccessDto
{
    /// <summary>
    ///     Id
    /// </summary>
    [ExcelColumn(Name = "异常出入记录编号", Index = 0, Width = 20)]
    public int Id { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    [ExcelColumn(Name = "姓名", Index = 1, Width = 20)]
    public string Name { get; set; }

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
    ///     当前状态,In为在院内 Out为在院外
    /// </summary>
    [ExcelColumn(Name = "当前状态", Index = 4, Width = 20)]
    public string Status { get; set; }

    /// <summary>
    ///     离开时间
    /// </summary>
    [ExcelColumn(Name = "离开时间", Index = 5, Width = 20)]
    public string LeaveTime { get; set; }

    /// <summary>
    ///     回来时间
    /// </summary>
    [ExcelColumn(Name = "回来时间", Index = 6, Width = 20)]
    public string ReturnTime { get; set; }

    /// <summary>
    ///     护理人员
    /// </summary>
    [ExcelColumn(Name = "护理人员", Index = 7, Width = 20)]
    public string Nurse { get; set; }

    /// <summary>
    ///     房间号
    /// </summary>
    [ExcelColumn(Name = "房间号", Index = 8, Width = 20)]
    public string RoomNo { get; set; }

    /// <summary>
    ///     请假状态
    /// </summary>
    [ExcelColumn(Name = "请假状态", Index = 9, Width = 20)]
    public string LeaveStatus { get; set; }
}