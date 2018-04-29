using AutoMapper;
using AutoMapper.Configuration;


namespace FailTracker.Web.Infrastructure.Mapping
{
	public interface IHaveCustomMappings
	{
		void CreateMappings(MapperConfigurationExpression configuration);
	}
}