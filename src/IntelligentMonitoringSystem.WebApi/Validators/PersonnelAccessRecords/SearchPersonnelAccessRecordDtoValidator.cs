using FluentValidation;
using IntelligentMonitoringSystem.Application.PersonnelAccessRecords.Dtos;

namespace IntelligentMonitoringSystem.WebApi.Validators.PersonnelAccessRecords;

/// <summary>
///     搜索人员出入记录参数验证器
/// </summary>
public class SearchPersonnelAccessRecordDtoValidator : AbstractValidator<SearchPersonnelAccessRequestDto>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SearchPersonnelAccessRecordDtoValidator" /> class.
    ///     构造函数
    /// </summary>
    public SearchPersonnelAccessRecordDtoValidator()
    {
        RuleFor(x => x.Name).MaximumLength(64);
        RuleFor(x => x.PageSize).NotNull();
        RuleFor(x => x.PageNo).NotNull();
    }
}