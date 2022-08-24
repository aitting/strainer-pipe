using Abp.StrainerPipe.Data;
using Abp.StrainerPipe.Transfer;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{
    public class BlobDataTaker : DataTaker, IDataTaker<byte[]>, ITransientDependency
    {
        public BlobDataTaker(IChannelTransfer channelTransfer) : base(channelTransfer)
        {
        }

        public override Type DataType => typeof(byte[]);

        public async Task<IEnumerable<IMetadata<byte[]>>> TakeAsync(int count = 1)
        {
            return await ChannelTransfer.TakeAsync<byte[]>(count);
        }

        public override async Task<IEnumerable<ObjectMetadata>> TakeObjectAsync(int count = 1)
        {
            var data = await TakeAsync(count);

            return data.Select(x => (ObjectMetadata)x.ToObject()).ToList();
        }
    }
}
