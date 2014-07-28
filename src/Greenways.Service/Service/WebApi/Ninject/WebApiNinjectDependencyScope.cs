using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Infrastructure.Disposal;
using Ninject.Parameters;
using Ninject.Syntax;

namespace Greenways.Service.WebApi.Ninject
{
    public class WebApiNinjectDependencyScope : DisposableObject, IDependencyScope
    {
        public WebApiNinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            ResolutionRoot = resolutionRoot;
        }

        protected IResolutionRoot ResolutionRoot { get; private set; }

        public object GetService(Type serviceType)
        {
            var request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ResolutionRoot.GetAll(serviceType).ToList();
        }
    }
}