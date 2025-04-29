using FluentValidation;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;
using static IntelligentMonitoringSystem.Domain.Shared.PersonnelAccessRecords.Enums.AccessStatusSmartEnum;

namespace IntelligentMonitoringSystem.WebApi.Validators.PersonnelAccessRecords;

/// <summary>
///     更新访问状态
/// </summary>
public class UpdateLeaveAccessStatusDtoValidator : AbstractValidator<UpdateLeaveAccessStatusDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateLeaveAccessStatusDtoValidator" /> class.
    ///     构造函数
    /// </summary>
    public UpdateLeaveAccessStatusDtoValidator()
    {
        RuleFor(x => x.AccessStatus).NotNull();
        RuleFor(x => x.AccessStatus).Must(x =>
            x == Abnormal || x == Normal).WithMessage("允许设置的值为正常或者异常");

        When(x => !string.IsNullOrWhiteSpace(x.Remark), () =>
        {
            RuleFor(x => x.Remark).MaximumLength(100);
        });
    }
}