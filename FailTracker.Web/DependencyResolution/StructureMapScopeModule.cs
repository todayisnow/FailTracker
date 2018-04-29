using System.Runtime.Remoting.Channels;

namespace FailTracker.Web.DependencyResolution {
    using System.Web;

    using FailTracker.Web.App_Start;

    using StructureMap.Web.Pipeline;
    using Infrastructure.Tasks;

    public class StructureMapScopeModule : IHttpModule {
        #region Public Methods and Operators

        public void Dispose() {
        }

        public void Init(HttpApplication context) {
           
            
            context.BeginRequest += (sender, e) =>
            {
                StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
                foreach (var task in StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetAllInstances<IRunOnEachRequest>())
                {
                    task.Execute();
                }
            };
            context.Error += (sender, e) =>
            {
                if (StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer != null)
                {
                    foreach (var task in StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer
                        .GetAllInstances<IRunOnError>())
                    {
                        task.Execute();
                    }
                }
                else
                {
                    StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
                    foreach (var task in StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer
                        .GetAllInstances<IRunOnError>())
                    {
                        task.Execute();
                    }
                    HttpContextLifecycle.DisposeAndClearAll();
                    StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
                }
            };
            context.EndRequest += (sender, e) => {
                try
                {
                    foreach (var task in StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.GetAllInstances<IRunAfterEachRequest>())
                    {
                        task.Execute();
                    }
                }
                finally
                {
                    HttpContextLifecycle.DisposeAndClearAll();
                    StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
                }
            };
        }

        #endregion
    }
}