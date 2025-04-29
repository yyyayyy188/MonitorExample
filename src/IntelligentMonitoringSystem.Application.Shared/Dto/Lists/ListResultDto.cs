using Newtonsoft.Json;

namespace IntelligentMonitoringSystem.Application.Shared.Dto.Lists;

/// <summary>
///     List result
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class ListResultDto<T> : IListResult<T>
{
    private IReadOnlyList<T>? _items;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListResultDto{T}" /> class.
    ///     Creates a new <see cref="ListResultDto{T}" /> object.
    /// </summary>
    public ListResultDto()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListResultDto{T}" /> class.
    ///     Creates a new <see cref="ListResultDto{T}" /> object.
    /// </summary>
    /// <param name="items">List of items</param>
    public ListResultDto(IReadOnlyList<T> items)
    {
        Items = items;
    }

    /// <summary>
    ///     Items
    /// </summary>
    [JsonProperty("items")]
    public IReadOnlyList<T> Items {
        get => _items ??= new List<T>();
        set => _items = value;
    }
}