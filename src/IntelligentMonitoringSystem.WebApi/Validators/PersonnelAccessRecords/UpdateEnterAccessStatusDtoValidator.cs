using FluentValidation;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

namespace IntelligentMonitoringSystem.WebApi.Validators.PersonnelAccessRecords;

/// <summary>
///     更新进入门禁记录状态验证
/// </summary>
public class UpdateEnterAccessStatusDtoValidator : AbstractValidator<UpdateEnterAccessStatusDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateEnterAccessStatusDtoValidator" /> class.
    ///     构造函数
    /// </summary>
    public UpdateEnterAccessStatusDtoValidator()
    {
        When(x => !string.IsNullOrWhiteSpace(x.Remark), () =>
        {
            RuleFor(x => x.Remark).MaximumLength(100);
        });
    }
}