using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace FailTracker.Web.DependencyResolution
{
    public class ActionFilterRegistry : Registry
    {
        #region Constructors and Destructors

        public ActionFilterRegistry()
        {

            For<IFilterProvider>().Singleton().Use<StructureMapFilterProvider>();
            Policies.SetAllProperties(x =>
                        x.Matching(p =>
                            p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                            p.DeclaringType.Namespace.StartsWith("FailTracker") &&
                            !p.PropertyType.IsPrimitive &&
                            p.PropertyType != typeof(string)));
        }

        #endregion
    }
}