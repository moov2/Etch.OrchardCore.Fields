using System;
using System.Text.Json;
using System.Threading.Tasks;
using Etch.OrchardCore.Fields.MultiSelect.Fields;
using Etch.OrchardCore.Fields.MultiSelect.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.MultiSelect.Settings
{
    public class MultiSelectFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<MultiSelectField>
    {
        #region Driver Methods

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition model, BuildEditorContext context)
        {
            return Initialize<EditMultiSelectFieldSettingsViewModel>("MultiSelectFieldSettings_Edit", viewModel =>
            {
                var settings = model.GetSettings<MultiSelectFieldSettings>();

                viewModel.Hint = settings.Hint;
                viewModel.Options = settings.Options;
                viewModel.OptionsJson = JConvert.SerializeObject(settings.Options ?? Array.Empty<string>());
            })
            .Location("Content");

        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var viewModel = new EditMultiSelectFieldSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix)) {
                context.Builder.WithSettings(new MultiSelectFieldSettings
                {
                    Hint = viewModel.Hint,
                    Options = string.IsNullOrWhiteSpace(viewModel.OptionsJson) ? Array.Empty<string>() : JConvert.DeserializeObject<string[]>(viewModel.OptionsJson)
                });
            }

            return Edit(model, context);
        }

        #endregion

        #endregion
    }
}

