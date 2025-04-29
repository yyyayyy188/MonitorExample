using Ardalis.SmartEnum;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;

namespace IntelligentMonitoringSystem.Domain.Shared.AccessDevices.Enums;

/// <summary>
///     设备类型
/// </summary>
public class AccessDeviceTypeSmartEnum : SmartEnum<AccessDeviceTypeSmartEnum>
{
    /// <summary>
    ///     进入监控设备
    /// </summary>
    public readonly static AccessDeviceTypeSmartEnum Enter = new EnterAccessDeviceTypeSmartEnum();

    /// <summary>
    ///     离开监控设备
    /// </summary>
    public readonly static AccessDeviceTypeSmartEnum Leave = new LeaveAccessDeviceTypeSmartEnum();

    /// <summary>
    ///     Initializes a new instance of the <see cref="AccessDeviceTypeSmartEnum" /> class.
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    public AccessDeviceTypeSmartEnum(string name, int value) : base(name, value)
    {
    }

    /// <summary>
    ///     获取设备对应的出入类型
    /// </summary>
    /// <returns>AccessTypeSmartEnum</returns>
    public virtual AccessTypeSmartEnum GetAccessType()
    {
        throw new NotImplementedException("类型转换未实现！");
    }

    /// <summary>
    ///     进入监控设备
    /// </summary>
    private sealed class EnterAccessDeviceTypeSmartEnum() : AccessDeviceTypeSmartEnum("Enter", 1)
    {
        /// <summary>
        ///     获取设备对应的出入类型
        /// </summary>
        /// <returns>Enter</returns>
        public override AccessTypeSmartEnum GetAccessType()
        {
            return AccessTypeSmartEnum.Enter;
        }
    }

    /// <summary>
    ///     离开监控设备
    /// </summary>
    private sealed class LeaveAccessDeviceTypeSmartEnum() : AccessDeviceTypeSmartEnum("Leave", 0)
    {
        /// <summary>
        ///     获取设备对应的出入类型
        /// </summary>
        /// <returns>Enter</returns>
        public override AccessTypeSmartEnum GetAccessType()
        {
            return AccessTypeSmartEnum.Leave;
        }
    }
}