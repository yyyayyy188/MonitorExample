using IntelligentMonitoringSystem.Application.Devices.Dtos;
using IntelligentMonitoringSystem.Application.Devices.List;
using IntelligentMonitoringSystem.Application.Devices.LiveStreaming;
using IntelligentMonitoringSystem.Application.Devices.LiveStreamingThumbnail;
using IntelligentMonitoringSystem.Application.Devices.VideoPlayback;
using IntelligentMonitoringSystem.Application.Shared.Dto;
using IntelligentMonitoringSystem.Domain.Shared.Exceptions;
using IntelligentMonitoringSystem.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentMonitoringSystem.WebApi.Controllers;

/// <summary>
///     TemplateController.
/// </summary>
/// <param name="sender">sender.</param>
[ApiController]
[CleanSvcRoute("/device")]
public class DeviceController(ISender sender) : ControllerBase
{
    /// <summary>
    ///     查询设备视频播放地址
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="beginTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns>视频播放地址</returns>
    [HttpGet("{deviceId}/video-playback")]
    public async Task<ResponseEntity<string>> VideoPlayback([FromRoute] string deviceId, [FromQuery] DateTime beginTime,
        [FromQuery] DateTime endTime)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            throw new UserFriendlyException("设备信息不存在");
        }

        var url = await sender.Send(new VideoPlaybackQuery { BeginTime = beginTime, EndTime = endTime, DeviceId = deviceId });
        return ResponseEntity<string>.Ok(url);
    }

    /// <summary>
    ///     获取设备直播信息截图
    /// </summary>
    /// <returns>视频播放地址</returns>
    [HttpGet("live-streaming-thumbnail")]
    public async Task<ResponseEntity<Dictionary<string, string>>> LiveStreamingThumbnail()
    {
        var url = await sender.Send(new LiveStreamingThumbnailQuery());
        return ResponseEntity<Dictionary<string, string>>.Ok(url);
    }

    /// <summary>
    ///     获取设备直播信息
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <returns>视频播放地址</returns>
    [HttpGet("{deviceId}/live-streaming")]
    public async Task<ResponseEntity<string>> LiveStreaming([FromRoute] string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            throw new UserFriendlyException("设备信息不存在");
        }

        var url = await sender.Send(new LiveStreamingQuery { DeviceId = deviceId });
        return ResponseEntity<string>.Ok(url);
    }

    /// <summary>
    ///     设备列表
    /// </summary>
    /// <returns>设备列表信息</returns>
    [HttpGet("list")]
    public async Task<ResponseEntity<MonitorCenterDeviceDto>> QueryDeviceList()
    {
        return ResponseEntity<MonitorCenterDeviceDto>.Ok(await sender.Send(new DeviceListQuery()));
    }
}