using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Etch.OrchardCore.Fields.Dictionary.Fields;
using Etch.OrchardCore.Fields.Dictionary.Models;
using Etch.OrchardCore.Fields.Dictionary.Settings;
using Etch.OrchardCore.Fields.Dictionary.ViewModels;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.Dictionary.Drivers
{
    public class DictionaryFieldDisplayDriver : ContentFieldDisplayDriver<DictionaryField>
    {
        #region PublicVariables

        public IStringLocalizer T { get; set; }

        #endregion PublicVariables

        #region Constructor

        public DictionaryFieldDisplayDriver(IStringLocalizer<DictionaryFieldDisplayDriver> localizer)
        {
            T = localizer;
        }

        #endregion Constructor

        #region Overrides

        #region Display

        public override IDisplayResult Display(DictionaryField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            return Initialize<DisplayDictionaryFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Field = field;
                model.Part = fieldDisplayContext.ContentPart;
                model.PartFieldDefinition = fieldDisplayContext.PartFieldDefinition;
                model.Data = field.Data;
            })
            .Location("Content")
            .Location("SummaryAdmin", "");
        }

        #endregion Display

        #region Edit

        public override IDisplayResult Edit(DictionaryField field, BuildFieldEditorContext context)
        {
            var settings = GetSettings(context);
            return Initialize<EditDictionaryFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;

                model.Data = JConvert.SerializeObject(field.Data ?? GetDefaults(context));

                model.MaxEntries = settings?.MaxEntries;
                model.MinEntries = settings?.MinEntries;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(DictionaryField field, UpdateFieldEditorContext context)
        {
            var model = new EditDictionaryFieldViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Data);

            var settings = GetSettings(context);

            field.Data = JConvert.DeserializeObject<List<DictionaryItem>>(model.Data);

            if (settings?.MinEntries > 0 && (field.Data == null || field.Data.Count < settings.MinEntries))
            {
                context.Updater.ModelState.AddModelError($"{Prefix}.{nameof(model.Data)}", T["You must specify at least {0} items.", settings.MinEntries]);
            }

            if (settings?.MaxEntries > 0 && field.Data?.Count > settings.MaxEntries)
            {
                context.Updater.ModelState.AddModelError($"{Prefix}.{nameof(model.Data)}", T["You can specify at most {0} items.", settings.MaxEntries]);
            }

            return Edit(field, context);
        }

        #endregion Edit

        #endregion Overrides

        #region Helpers

        private static DictionaryFieldSettings GetSettings(BuildFieldEditorContext context)
        {
            return context.PartFieldDefinition.GetSettings<DictionaryFieldSettings>();
        }

        private static IList<DictionaryItem> GetDefaults(BuildFieldEditorContext context)
        {
            var settingsValue = GetSettings(context)?.DefaultData;

            if (settingsValue != null)
            {
                return JConvert.DeserializeObject<IList<DictionaryItem>>(settingsValue);
            }

            return new List<DictionaryItem>();
        }

        #endregion Helpers
    }
}
