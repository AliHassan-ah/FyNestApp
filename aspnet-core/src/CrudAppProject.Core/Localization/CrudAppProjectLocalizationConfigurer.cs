using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace CrudAppProject.Localization
{
    public static class CrudAppProjectLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CrudAppProjectConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CrudAppProjectLocalizationConfigurer).GetAssembly(),
                        "CrudAppProject.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
