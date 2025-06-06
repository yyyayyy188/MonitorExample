﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2024/11/27 10:27:56
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable
using Ardalis.GuardClauses;
using IntelligentMonitoringSystem.Domain.AccessDevices;
using IntelligentMonitoringSystem.Domain.LeaveApplications;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Events;
using IntelligentMonitoringSystem.Domain.Shared.Contracts.Entities;
using IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums;
using MassTransit;

namespace IntelligentMonitoringSystem.Domain.PersonnelAccessRecords;

/// <summary>
///     出入记录表
/// </summary>
public partial class PersonnelAccessRecord : AggregateRoot
{
    /// <summary>
    ///     Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     面容Id
    /// </summary>
    public string FaceId { get; set; }

    /// <summary>
    ///     姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     年龄
    /// </summary>
    public short Age { get; set; }

    /// <summary>
    ///     性别
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    ///     访问类型
    /// </summary>
    public AccessTypeSmartEnum AccessType { get; set; }

    /// <summary>
    ///     出入状态(P:暂无,S:正常外出,E异常外出,R:返回)
    /// </summary>
    public AccessStatusSmartEnum AccessStatus { get; set; }

    /// <summary>
    ///     出入时间
    /// </summary>
    public DateTime AccessTime { get; set; }

    /// <summary>
    ///     原始头像图片路径
    /// </summary>
    public string OriginalFaceUrl { get; set; }

    /// <summary>
    ///     原始抓拍图片
    /// </summary>
    public string OriginalSnapUrl { get; set; }

    /// <summary>
    ///     原始面容图片地址
    /// </summary>
    public string RecognitionFaceOssUrl { get; set; }

    /// <summary>
    ///     头像图片路径
    /// </summary>
    public string? FaceImgPath { get; set; }

    /// <summary>
    ///     抓拍图片路径
    /// </summary>
    public string? SnapImgPath { get; set; }

    /// <summary>
    ///     原始面容图片地址路径
    /// </summary>
    public string? RecognitionFacePath { get; set; }

    /// <summary>
    ///     备注信息
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     主题报警
    /// </summary>
    public TopicAlarmEventLog TopicAlarmEventLog { get; private set; }

    /// <summary>
    ///     异常出入记录
    /// </summary>
    public AbnormalPersonnelAccessRecord? AbnormalPersonnelAccessRecord { get; set; }

    /// <summary>
    ///     护理人员
    /// </summary>
    public string Nurse { get; set; }

    /// <summary>
    ///     房间号
    /// </summary>
    public string RoomNo { get; set; }

    #region 构造函数

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonnelAccessRecord" /> class.
    ///     创建出入记录表信息
    /// </summary>
    /// <param name="accessDevice">设备新信息</param>
    /// <param name="faceId">面容Id</param>
    /// <param name="name">姓名</param>
    /// <param name="age">年龄</param>
    /// <param name="gender">性别</param>
    /// <param name="accessTime">进出时间</param>
    /// <param name="originalSnapUrl">原始抓拍图片</param>
    /// <param name="originalFaceUrl">原始面容图片</param>
    /// <param name="recognitionFaceOssUrl">原始采集图片地址</param>
    /// <param name="leaveApplication">请假申请单信息</param>
    public PersonnelAccessRecord(AccessDevice accessDevice, string faceId, string name, short age,
        string gender, DateTime accessTime, string originalSnapUrl, string originalFaceUrl,
        string recognitionFaceOssUrl, LeaveApplication? leaveApplication)
    {
        EventIdentifier = NewId.NextGuid();
        CreateTime = DateTime.Now;

        FaceId = Guard.Against.NullOrWhiteSpace(faceId, nameof(faceId));
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Age = age;
        Gender = Guard.Against.NullOrWhiteSpace(gender, nameof(gender));
        AccessTime = Guard.Against.Null(accessTime, nameof(accessTime));

        // 设置原始图片
        SetOriginalImgPath(originalSnapUrl, originalFaceUrl, recognitionFaceOssUrl);

        // 设置设备信息
        SetDeviceInfo(accessDevice);

        // 设置请假单信息
        SetLeaveApplication(leaveApplication);

        // 同步信息至User
        AddEvent(new DomainEventRecord(new SynchronizationUserEvent(FaceId, AccessType, AccessTime)));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonnelAccessRecord" /> class.
    ///     同步图片信息
    /// </summary>
    /// <param name="eventIdentifier">事件标识</param>
    /// <param name="faceImgPath">面部图片地址</param>
    /// <param name="snapImgPath">抓拍图片地址</param>
    public PersonnelAccessRecord(Guid eventIdentifier, string faceImgPath, string snapImgPath)
    {
        EventIdentifier = eventIdentifier;
        FaceImgPath = faceImgPath;
        SnapImgPath = snapImgPath;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonnelAccessRecord" /> class.
    ///     同步图片信息
    /// </summary>
    /// <param name="id">id</param>
    public PersonnelAccessRecord(int id)
    {
        Id = id;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PersonnelAccessRecord" /> class.
    ///     默认无参构造函数
    /// </summary>
    public PersonnelAccessRecord() { }

    #endregion

    #region 设备信息

    /// <summary>
    ///     设备Id
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    ///     设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    ///     设备播放地址
    /// </summary>
    public string? DevicePlayback { get; set; }

    #endregion

    #region 请假状态

    /// <summary>
    ///     请假状态(W:未请假,S:请假通过,F请假未通过)
    /// </summary>
    public LeaveStatusSmartEnum? LeaveStatus { get; set; }

    /// <summary>
    ///     请假记录Id
    /// </summary>
    public int? LeaveApplicationId { get; set; }

    /// <summary>
    ///     预计返回时间
    /// </summary>
    public DateTime? ExpectedReturnTime { get; set; }

    #endregion

    #region 审计信息

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreateTime { get; private set; }

    /// <summary>
    ///     最后操作时间
    /// </summary>
    public DateTime? LastEditTime { get; set; }

    /// <summary>
    ///     最后编辑用户Id
    /// </summary>
    public string? LastEditUserId { get; set; }

    /// <summary>
    ///     最后编辑用户名
    /// </summary>
    public string? LastEditUserName { get; set; }

    /// <summary>
    ///     事件溯源唯一标识
    /// </summary>
    public Guid EventIdentifier { get; set; }

    #endregion
}