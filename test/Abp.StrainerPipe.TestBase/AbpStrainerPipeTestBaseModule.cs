using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Threading;
using Volo.Abp.BackgroundJobs;

namespace Abp.StrainerPipe.TestBase
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpStrainerPipeCoreModule),
        typeof(AbpBackgroundJobsAbstractionsModule)
    )]

    public class AbpStrainerPipeTestBaseModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            //PreConfigure<AbpIdentityServerBuilderOptions>(options =>
            //{
            //    options.AddDeveloperSigningCredential = false;
            //});

            //PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            //{
            //    identityServerBuilder.AddDeveloperSigningCredential(false, System.Guid.NewGuid().ToString());
            //});
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            context.Services.AddAlwaysAllowAuthorization();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            //AsyncHelper.RunSync(async () =>
            //{
            //    using (var scope = context.ServiceProvider.CreateScope())
            //    {
            //        await scope.ServiceProvider
            //            .GetRequiredService<IDataSeeder>()
            //            .SeedAsync();
            //    }
            //});
        }
    }
}