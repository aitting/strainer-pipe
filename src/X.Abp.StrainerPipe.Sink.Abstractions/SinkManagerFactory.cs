using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abp.StrainerPipe
{


    public class SinkManagerFactory : ISinkManagerFactory
    {

        public virtual Dictionary<string, ISinkManager> SinkManagers { get; private set; }

        public IServiceProvider ServiceProvider { get; set; }

        public SinkManagerFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            SinkManagers = new Dictionary<string, ISinkManager>();
        }

        public virtual ISinkManager CreateInstance()
        {
            return ServiceProvider.GetRequiredService<ISinkManager>();
        }

        public ISinkManager GetOrCreate(Guid? tenantId = null, bool createNew = false)
        {
            var dicKey = GetDicKey(tenantId);
            if (!SinkManagers.ContainsKey(dicKey))
            {
                SinkManagers.Add(dicKey, CreateInstance());
            }
            else if (createNew)
            {
                SinkManagers[dicKey] = CreateInstance();
            }

            return SinkManagers[dicKey];
        }

        private string GetDicKey(Guid? tenantId = null)
        {
            if (!tenantId.HasValue)
            {
                return "host";
            }

            return tenantId.Value.ToString("N");
        }

        public async Task CreateAndStartAsync(Guid? tenantId = null)
        {
            var instance = GetOrCreate(tenantId);
            await instance.StartSinkAsync(tenantId);
        }
    }
}
