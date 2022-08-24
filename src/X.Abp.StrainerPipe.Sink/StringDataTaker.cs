using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class StringDataTaker : DataTaker, IDataTaker<string>, ITransientDependency
    {
        public StringDataTaker(IChannelTransfer channelTransfer) : base(channelTransfer)
        {
        }

        public override Type DataType => typeof(string);

        public async Task<IEnumerable<IMetadata<string>>> TakeAsync(int count = 1)
        {
            return await ChannelTransfer.TakeAsync<string>(count);
        }

        public override async Task<IEnumerable<ObjectMetadata>> TakeObjectAsync(int count = 1)
        {
            var data = await TakeAsync(count);

            return data.Select(x => (ObjectMetadata)x.ToObject()).ToList();
        }
    }
}
