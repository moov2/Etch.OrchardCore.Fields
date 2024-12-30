using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Etch.OrchardCore.Fields.ResponsiveMedia.Fields;
using Etch.OrchardCore.Fields.ResponsiveMedia.Models;
using Etch.OrchardCore.Fields.ResponsiveMedia.Settings;
using Etch.OrchardCore.Fields.ResponsiveMedia.Utils;
using Etch.OrchardCore.Fields.ResponsiveMedia.ViewModels;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Media;

namespace Etch.OrchardCore.Fields.ResponsiveMedia.Drivers
{
    public class ResponsiveMediaFieldDisplayDriver : ContentFieldDisplayDriver<ResponsiveMediaField>
    {
        #region Dependencies

        private readonly IMediaFileStore _mediaFileStore;

        public IStringLocalizer S { get; set; }

        #endregion

        #region Constructor

        public ResponsiveMediaFieldDisplayDriver(IMediaFileStore mediaFileStore, IStringLocalizer<ResponsiveMediaFieldDisplayDriver> localizer)
        {
            _mediaFileStore = mediaFileStore;
            S = localizer;
        }

        #endregion

        public override IDisplayResult Display(ResponsiveMediaField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            var settings = fieldDisplayContext.PartFieldDefinition.GetSettings<ResponsiveMediaFieldSettings>();
            var data = field.HasData ? field.Data : settings.FallbackData;

            return Initialize<DisplayResponsiveMediaFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Field = field;
                model.Part = fieldDisplayContext.ContentPart;
                model.PartFieldDefinition = fieldDisplayContext.PartFieldDefinition;
                model.Media = ParseMedia(data, settings.GetBreakpoints());
            })
            .Location("Content")
            .Location("SummaryAdmin", "")
            .Location("DetailAdmin", ""); 
        }

        public override IDisplayResult Edit(ResponsiveMediaField field, BuildFieldEditorContext context)
        {
            var settings = context.PartFieldDefinition.GetSettings<ResponsiveMediaFieldSettings>();

            if (!settings.IsConfigured)
            {
                return Initialize<EditResponsiveMediaFieldViewModel>("ResponsiveMediaField_Unconfigured", model =>
                {
                    model.Field = field;
                    model.Part = context.ContentPart;
                    model.PartFieldDefinition = context.PartFieldDefinition;
                });
            }

            return Initialize<EditResponsiveMediaFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;
                model.Data = JConvert.SerializeObject(ResponsiveMediaUtils.ParseMedia(_mediaFileStore, field.Data));
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ResponsiveMediaField field, UpdateFieldEditorContext context)
        {
            if (await context.Updater.TryUpdateModelAsync(field, Prefix, f => f.Data))
            {
                var settings = context.PartFieldDefinition.GetSettings<ResponsiveMediaFieldSettings>();

                if (settings.Required && !JConvert.DeserializeObject<IList<ResponsiveMediaItem>>(field.Data).Any())
                {
                    context.Updater.ModelState.AddModelError(Prefix, S["{0}: Media is required.", context.PartFieldDefinition.DisplayName()]);
                }

            }

            return Edit(field, context);
        }
        
        public IList<ResponsiveMediaItem> ParseMedia(string data, int[] breakpoints)
        {
            var media = ResponsiveMediaUtils.ParseMedia(_mediaFileStore, data);

            foreach (var mediaItem in media)
            {
                mediaItem.Sources = mediaItem.GetSourceSets(breakpoints);
            }

            return media;
        }
    }
}
