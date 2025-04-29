using FluentValidation;
using IntelligentMonitoringSystem.Application.AbnormalPersonnelAccessRecords.Dtos;

namespace IntelligentMonitoringSystem.WebApi.Validators.AbnormalPersonnelAccessRecords;

/// <summary>
///     搜索异常人员出入记录请求参数验证器
/// </summary>
public class SearchAbnormalPersonnelAccessRequestDtoValidator : AbstractValidator<SearchAbnormalPersonnelAccessRequestDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchAbnormalPersonnelAccessRequestDtoValidator" /> class.
    ///     构造函数
    /// </summary>
    public SearchAbnormalPersonnelAccessRequestDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(64);
        RuleFor(x => x.PageSize).NotNull();
        RuleFor(x => x.PageNo).NotNull();

        When(x => x.LeaveStartTime.HasValue || x.LeaveEndTime.HasValue, () =>
        {
            RuleFor(x => x.LeaveStartTime).NotNull();
            RuleFor(x => x.LeaveEndTime).NotNull();
        });

        When(x => x.EnterEndTime.HasValue || x.EnterStartTime.HasValue, () =>
        {
            RuleFor(x => x.EnterEndTime).NotNull();
            RuleFor(x => x.EnterStartTime).NotNull();
        });
    }
}