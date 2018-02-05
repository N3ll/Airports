[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ABS_v3.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ABS_v3.App_Start.NinjectWebCommon), "Stop")]

namespace ABS_v3.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                kernel.Bind(x =>
                {
                    x.From("ABS_v2.BusinessLogic")
                   .SelectAllClasses()
                   .BindDefaultInterface();
                });

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // kernel.Bind(typeof(ISystemManager)).To(typeof(SystemManager));

            //kernel.Bind(typeof(IAirportManager<>)).To(typeof(AirportManager));
            //kernel.Bind(typeof(IAirlineManager<>)).To(typeof(AirlineManager));
            //kernel.Bind(typeof(IFilterManager<>)).To(typeof(FilterManager));
            //kernel.Bind(typeof(IFlightManager<>)).To(typeof(FlightManager));
            //kernel.Bind(typeof(IFlightSectionManager<>)).To(typeof(FlightSectionManager));
            //kernel.Bind(typeof(ISeatManager<>)).To(typeof(SeatManager));
            //kernel.Bind(typeof(ISectionClassManager<>)).To(typeof(SectionClassManager));
        }
    }
}
