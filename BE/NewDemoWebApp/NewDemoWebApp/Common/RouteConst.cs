namespace NewDemoWebApp.Common
{
    public class RouteConst
    {
        public const string defaultRoute = "api/[controller]";
        //[Route("[controller]")]
        public class Employee
        {
            public const string Search = defaultRoute;
            public const string Update = defaultRoute+"/{id}";
            public const string Delete = defaultRoute+"/{id}";
            public const string Create = defaultRoute;
        }
    }
}
