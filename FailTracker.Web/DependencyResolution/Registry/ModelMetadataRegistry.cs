using System.Web.Mvc;
using FailTracker.Web.Infrastructure.ModelMetadata;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace FailTracker.Web.DependencyResolution
{
	public class ModelMetadataRegistry : Registry
	{
		public ModelMetadataRegistry()
		{
			For<ModelMetadataProvider>().Use<ExtensibleModelMetadataProvider>();

			Scan(scan =>
			{
				scan.TheCallingAssembly();
				scan.AddAllTypesOf<IModelMetadataFilter>();
			});
		}
	}
}