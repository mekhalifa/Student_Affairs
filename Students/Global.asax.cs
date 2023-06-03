using Autofac;
using Autofac.Integration.Mvc;
using BAL.Interfaces;
using BAL.Services;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repos;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Students
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerDependency();
            builder.RegisterType(typeof(StudentRepository)).As(typeof(IStudentRepository));
            builder.RegisterType(typeof(ClassRepository)).As(typeof(IClassRepository));
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork));
            builder.RegisterType(typeof(StudentService)).As(typeof(IStudentService));
            builder.RegisterType(typeof(ClassService)).As(typeof(IClassService));
            builder.RegisterType<StudentAffairsDbContext>().AsImplementedInterfaces().AsSelf();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}
