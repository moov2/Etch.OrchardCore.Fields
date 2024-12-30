using System.Threading.Tasks;
using Etch.OrchardCore.Fields.ResponsiveMedia.Fields;
using Etch.OrchardCore.Fields.ResponsiveMedia.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;

namespace Etch.OrchardCore.Fields.ResponsiveMedia
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IContentDefinitionManager _contentDefinitionManager;

        #endregion Dependencies

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        #endregion Constructor

        #region Migrations

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.MigrateFieldSettingsAsync<ResponsiveMediaField, ResponsiveMediaFieldSettings>();

            return 1;
        }

        #endregion Migrations
    }
}
