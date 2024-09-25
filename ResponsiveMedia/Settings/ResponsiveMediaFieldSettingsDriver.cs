using System.Text.Json;
using System.Threading.Tasks;
using Etch.OrchardCore.Fields.ResponsiveMedia.Fields;
using Etch.OrchardCore.Fields.ResponsiveMedia.Utils;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Media;

namespace Etch.OrchardCore.Fields.ResponsiveMedia.Settings
{
    public class ResponsiveMediaFieldSettingsDriver : ContentPartFieldDefinitionDisplayDriver<ResponsiveMediaField>
    {
        #region Dependencies

        private readonly IMediaFileStore _mediaFileStore;
        private readonly IStringLocalizer<ResponsiveMediaFieldSettingsDriver> T;

        #endregion

        #region Constructor

        public ResponsiveMediaFieldSettingsDriver(IStringLocalizer<ResponsiveMediaFieldSettingsDriver> localizer, IMediaFileStore mediaFileStore)
        {
            T = localizer;
            _mediaFileStore = mediaFileStore;
        }

        #endregion

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition, BuildEditorContext context)
        {
            return Initialize<ResponsiveMediaFieldSettings>("NumericFieldSettings_Edit", model =>
            {
                var settings = partFieldDefinition.GetSettings<ResponsiveMediaFieldSettings>();
                model.AllowMediaText = settings.AllowMediaText;
                model.Breakpoints = settings.Breakpoints;
                model.FallbackData = settings.FallbackData;
                model.Hint = settings.Hint;
                model.LazyLoad = settings.LazyLoad;
                model.Multiple = settings.Multiple;
                model.Required = settings.Required;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition model, UpdatePartFieldEditorContext context)
        {
            var viewModel = new UpdateResponsiveMediaFieldSettingsViewModel();

            await context.Updater.TryUpdateModelAsync(viewModel, Prefix);

            var settings = new ResponsiveMediaFieldSettings
            {
                AllowMediaText = viewModel.AllowMediaText,
                Hint = viewModel.Hint,
                LazyLoad = viewModel.LazyLoad,
                Multiple = viewModel.Multiple,
                Required = viewModel.Required,
                FallbackData = JConvert.SerializeObject(ResponsiveMediaUtils.ParseMedia(_mediaFileStore, viewModel.FallbackData))
            };

            try
            {
                settings.Breakpoints = viewModel.Breakpoints;
                settings.GetBreakpoints();
            } catch
            {
                context.Updater.ModelState.AddModelError(Prefix, T["Failed to parse breakpoints, make sure it only contains numeric values."]);
            }

            context.Builder.WithSettings(settings);

            return Edit(model, context);
        }
    }
}
