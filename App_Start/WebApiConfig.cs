using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace ProductsApp
{
    using System.Web.Http.Controllers;

    using Extension;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //another way for serizalizer
            config.MessageHandlers.Add(new SerializerHandler());

            //Dto permission handler
            config.MessageHandlers.Add(new DtoPermissionHandler());

            //exception handler
            config.Filters.Add(new CustomExceptionFilterAttribute());

            //authentication related. Oauth
            config.Filters.Add(new AuthenticationFilter());

            //operation level authorization
            config.Filters.Add(new OperationAuthorizeAttribute());

            //out put cache
            config.Services.Replace(typeof(IHttpActionInvoker), new FabricCacheActionInvoker());
            config.Services.Replace(typeof(IHttpActionSelector), new WsActionSelector());
            ////config.Services.Replace(typeof(IHttpControllerSelector), new WsControllerSelector(config));

            //serialize
            config.Formatters.Add(new ProtobufMediaTypeFormatter());

            //ObjectFactoryConfiguration.Config();
            //config.DependencyResolver = new DependencyResolver(ObjectFactory.Builder);
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });

            //config.Routes.MapHttpRoute(
            //    name: "VersionApi",
            //    routeTemplate: "version/{action}",
            //    defaults: new { controller = "test" });

            //config.Routes.MapHttpRoute(
            //    name: "VersionUrlRoute",
            //    routeTemplate: "rest/v{api_version}/{*others}");
        }
    }
}
