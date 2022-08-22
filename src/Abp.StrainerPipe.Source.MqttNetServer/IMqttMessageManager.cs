﻿using Volo.Abp.DependencyInjection;

namespace Abp.StrainerPipe
{

    public interface IMqttMessageManager : ISingletonDependency, IDisposable
    {
        Task PutAsync(string topic, string message);

        Task<IEnumerable<MqttMessageDto>> TakeAsync(int count = 1);
    }
}