// <copyright file="IFileServerApi.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers.Models;
using Refit;

namespace IntelligentMonitoringSystem.Infrastructure.Reference.Apis.FileServers;

/// <summary>
///     文件服务器接口.
/// </summary>
public interface IFileServerApi
{
    /// <summary>
    ///     上传文件.
    /// </summary>
    /// <param name="fileInfoPart">fileInfoPart.</param>
    /// <param name="fileType">文件类型</param>
    /// <returns>HttpResponseMessage.</returns>
    [Multipart]
    [Post("/capability/files/upload")]
    Task<ApiResponse<FileServerBaseResponse<FileServerUploadResponse>>> UploadFileAsync(
        MultipartFormDataContent fileInfoPart,
        [AliasAs("fileType")] string fileType = "imgs");
}