### Migrations
```csharp
添加迁移
dotnet ef migrations add Initial --project src\IntelligentMonitoringSystem.Infrastructure\IntelligentMonitoringSystem.Infrastructure.csproj --startup-project src\IntelligentMonitoringSystem.WebApi\IntelligentMonitoringSystem.WebApi.csproj --context IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.TemplateDbContext --output-dir EntityFrameworkCore\Migrations

生成迁移脚本
dotnet ef migrations script --project src\IntelligentMonitoringSystem.Infrastructure\IntelligentMonitoringSystem.Infrastructure.csproj --startup-project src\IntelligentMonitoringSystem.WebApi\IntelligentMonitoringSystem.WebApi.csproj --context IntelligentMonitoringSystem.Infrastructure.EntityFrameworkCore.TemplateDbContext --configuration Debug 0 20241114024541_Initial
```    
