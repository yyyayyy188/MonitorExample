using System.Reflection;
using Ardalis.SmartEnum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IntelligentMonitoringSystem.WebApi.Extensions;

/// <summary>
///     SmartEnumDocumentFilter
/// </summary>
/// <param name="assembliesToScan">assembliesToScan</param>
public class SmartEnumDocumentFilter(List<Assembly> assembliesToScan) : IDocumentFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var smartEnumTypes = assembliesToScan
            .SelectMany(a => a.GetTypes())
            .Where(t => IsTypeDerivedFromGenericType(t, typeof(SmartEnum<,>)) ||
                        !IsTypeDerivedFromGenericType(t, typeof(SmartEnum<>)));

        foreach (var smartEnumType in smartEnumTypes)
        {
            if (!context.SchemaRepository.Schemas.TryGetValue(smartEnumType.Name!, out var schema))
            {
                // The smart enum is not mentioned in the Swagger definition
                continue;
            }

            schema.Required = null;
            schema.Properties = null;
            schema.AdditionalProperties = null;

            var enumValues = smartEnumType
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                .Select(x => x.Name);

            var openApiValues = new OpenApiArray();
            openApiValues.AddRange(enumValues.Select(x => new OpenApiString(x)));

            schema.Type = "string";
            schema.Enum = openApiValues;
        }
    }

    private static bool IsTypeDerivedFromGenericType(Type typeToCheck, Type genericType)
    {
        if (typeToCheck == typeof(object))
        {
            return false;
        }

        if (typeToCheck == null)
        {
            return false;
        }

        if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }

        return IsTypeDerivedFromGenericType(typeToCheck.BaseType, genericType);
    }
}