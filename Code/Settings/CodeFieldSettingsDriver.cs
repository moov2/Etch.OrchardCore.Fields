using System.Threading.Tasks;
using Etch.OrchardCore.Fields.Code.Fields;
using Etch.OrchardCore.Fields.Code.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.Code.Settings
{
    public class CodeFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<CodeField>
    {
        #region Driver Methods

        #region Edit

        public override IDisplayResult Edit(ContentPartFieldDefinition model, BuildEditorContext context)
        {
            return Initialize<EditCodeFieldSettingsViewModel>("CodeFieldSettings_Edit", viewModel =>
            {
                viewModel.Language = model.GetSettings<CodeFieldSettings>().Language;

                if (string.IsNullOrWhiteSpace(viewModel.Language))
                {
                    viewModel.Language = Constants.DefaultLanguage;
                }
            })
            .Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var viewModel = new EditCodeFieldSettingsViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                context.Builder.WithSettings(new CodeFieldSettings
                {
                    Language = viewModel.Language
                });
            }

            return Edit(model, context);
        }

        #endregion

        #endregion
    }
}
