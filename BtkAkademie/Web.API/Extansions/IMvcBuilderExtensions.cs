using Web.API.Utilities.Formatters;

namespace Web.API.Extansions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCvsFormantter(this IMvcBuilder builder)=>
            builder.AddMvcOptions(config =>
                config.OutputFormatters
                .Add(new CsvOutputFormatter()));
    }
}
