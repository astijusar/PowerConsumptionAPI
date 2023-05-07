using AutoMapper;

namespace PowerConsumptionAPI.Models.Mapping
{
    public class StringToTimeOnlyConverter : ITypeConverter<string, TimeOnly>
    {
        public TimeOnly Convert(string source, TimeOnly destination, ResolutionContext context)
        {
            if (TimeOnly.TryParse(source, out TimeOnly result))
            {
                return result;
            }

            return default;
        }
    }
}
