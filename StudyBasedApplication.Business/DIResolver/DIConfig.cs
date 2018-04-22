using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using StudyBasedApplication.Data.Repository;
using StudyBasedApplication.Data.EFRepository;
using StudyBasedApplication.Business;
using StudyBasedApplication.Models;

namespace StudyBasedApplication.UI.MVC.App_Start
{

    public class NInjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        Ninject.IKernel kernel = null;

        public NInjectDependencyResolver()
        {
            this.kernel = new StandardKernel();
            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            BindDependencies();
        }

        public NInjectDependencyResolver(Ninject.IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public void BindDependencies()
        {
            //Define the dependencies here
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<IPageManager>().To<PageManager>();
            kernel.Bind<IGenericGetter<UserGroup>>().To<GenericGetter<UserGroup>>();
            kernel.Bind<IPrimaryDBGetters>().To<PrimaryDBGetters>();
            kernel.Bind<IGenericGetter<DataSource>>().To<GenericGetter<DataSource>>();
            kernel.Bind<IContentMapper<DataSourceStudyStatus, LocalStudyStatus>>().To<StudyStatusMapper>();
            kernel.Bind<IGenericGetter<StudyStatusMapping>>().To<GenericGetter<StudyStatusMapping>>();
            kernel.Bind<ISponsorManager>().To<SponsorManager>();
            kernel.Bind<IStudyManager>().To<StudyManager>();
            kernel.Bind<INavigationLog>().To<NavigationManager>();
            kernel.Bind<IGenericGetter<User>>().To<GenericGetter<User>>();
            //kernel.Bind<>().To<>();
            //kernel.Bind<>().To<>();
            //kernel.Bind<>().To<>();
            //kernel.Bind<>().To<>();
            //kernel.Bind<>().To<>();
            //kernel.Bind<>().To<>();

        }

    }

}