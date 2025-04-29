using FFMpegCore;
using FFMpegCore.Enums;
using IntelligentMonitoringSystem.Domain.PersonnelAccessRecords.Repositories;

namespace IntelligentMonitoringSystem.Infrastructure.FFMpeg.Repositories;

/// <summary>
///     视频转码
/// </summary>
public class TransformVideoRepository : ITransformVideoRepository
{
    /// <summary>
    ///     转码视频
    /// </summary>
    /// <param name="videoPath">视频路径</param>
    /// <param name="temporaryFilePath">临时文件路径</param>
    /// <returns>流</returns>
    public async Task<bool> TransformVideoAsync(string videoPath, string temporaryFilePath)
    {
        // 获取临时文件所在的目录路径
        var directoryPath = Path.GetDirectoryName(temporaryFilePath);

        // 检查目录是否存在，如果不存在则创建
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var result = await FFMpegArguments
            .FromUrlInput(new Uri(videoPath))
            .OutputToFile(temporaryFilePath, true, options =>
            {
                options.WithAudioCodec(AudioCodec.Aac)
                    .WithVideoFilters(filterOptions => filterOptions
                        .Scale(VideoSize.Hd))
                    ;
            })
            .ProcessAsynchronously();
        return result;
    }
}