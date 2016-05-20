using Owin;
using System;

namespace Greenways.Service.Owin
{
    public interface IOwinService : IDisposable
    {
        int Order { get; }

        void Configuration(IAppBuilder appBuilder);
    }
}
