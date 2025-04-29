using FluentValidation;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

namespace IntelligentMonitoringSystem.WebApi.Validators.PersonnelAccessRecords;

/// <summary>
///     导出出入记录请求参数验证器
/// </summary>
public class ExportPersonnelAccessRequestDtoValidator : AbstractValidator<ExportPersonnelAccessRequestDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ExportPersonnelAccessRequestDtoValidator" /> class.
    ///     构造函数
    /// </summary>
    public ExportPersonnelAccessRequestDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(64);
    }
}