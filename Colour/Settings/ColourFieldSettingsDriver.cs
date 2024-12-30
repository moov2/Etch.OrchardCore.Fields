using Etch.OrchardCore.Fields.Colour.Fields;
using Etch.OrchardCore.Fields.Colour.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Text.Json;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Colour.Settings
{
    public class ColourFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<ColourField>
    {
        #region Driver Methods

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition model, BuildEditorContext context)
        {
            return Initialize<EditColourFieldSettingsViewModel>("ColourFieldSettings_Edit", viewModel =>
            {
                var settings = model.GetSettings<ColourFieldSettings>();

                viewModel.AllowCustom = settings.AllowCustom;
                viewModel.AllowTransparent = settings.AllowTransparent;
                viewModel.Colours = JConvert.SerializeObject(settings.Colours);
                viewModel.DefaultValue = settings.DefaultValue;
                viewModel.Hint = settings.Hint;
                viewModel.UseGlobalColours = settings.UseGlobalColours;
            })
            .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var viewModel = new EditColourFieldSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                context.Builder.WithSettings(new ColourFieldSettings
                {
                    AllowCustom = viewModel.AllowCustom,
                    AllowTransparent = viewModel.AllowTransparent,
                    Colours = JConvert.DeserializeObject<ColourItem[]>(viewModel.Colours),
                    DefaultValue = viewModel.DefaultValue,
                    Hint = viewModel.Hint,
                    UseGlobalColours = viewModel.UseGlobalColours
                });
            }

            return Edit(model, context);
        }

        #endregion

        #endregion
    }
}
