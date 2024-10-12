using ModularCrm.Products;
using Volo.Abp.AutoMapper;
using Volo.Abp.Application;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Domain;
using ModularCrm.Ordering.Contracts;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;

namespace ModularCrm.Ordering;

[DependsOn(
    typeof(ProductsApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpEntityFrameworkCoreModule),
    typeof(AbpDddDomainModule),
    typeof(ModularCrmOrderingContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule)
)]
public class OrderingWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(OrderingWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<OrderingWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<OrderingWebModule>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new OrderingMenuContributor());
        });
    }
}