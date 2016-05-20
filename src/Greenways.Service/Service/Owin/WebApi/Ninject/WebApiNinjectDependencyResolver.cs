using System.Web.Http.Dependencies;
using Ninject.Syntax;

namespace Greenways.Service.WebApi.Ninject
{
    public class WebApiNinjectDependencyResolver : WebApiNinjectDependencyScope, IDependencyResolver
    {
        public WebApiNinjectDependencyResolver(IResolutionRoot resolutionRoot)
            : base(resolutionRoot)
        {
        }

        public virtual IDependencyScope BeginScope()
        {
            return this;
        }
    }
}