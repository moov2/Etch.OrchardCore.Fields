using Etch.OrchardCore.Fields.Values.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Values.Settings
{
    public class ValuesFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<ValuesField>
    {
        #region Driver Methods

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition, BuildEditorContext context)
        {
            return Initialize<ValuesFieldSettings>("ValuesFieldSettings_Edit", model =>
            {
                var settings = partFieldDefinition.GetSettings<ValuesFieldSettings>();
                model.EmptyMessage = settings.EmptyMessage;
                model.Hint = settings.Hint;
                model.NewItemPlaceholder = settings.NewItemPlaceholder;
            })
            .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var settings = new ValuesFieldSettings();

            if (await context.Updater.TryUpdateModelAsync(settings, Prefix))
            {
                context.Builder.WithSettings(settings);
            }

            return Edit(model, context);
        }

        #endregion

        #endregion
    }
}

