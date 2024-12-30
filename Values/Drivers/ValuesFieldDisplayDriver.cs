using Microsoft.Extensions.Localization;
using Etch.OrchardCore.Fields.Values.Fields;
using Etch.OrchardCore.Fields.Values.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Etch.OrchardCore.Fields.Values.Drivers
{
    public class ValuesFieldDisplayDriver : ContentFieldDisplayDriver<ValuesField>
    {
        #region Dependencies

        public IStringLocalizer T { get; set; }

        #endregion

        #region Constructor

        public ValuesFieldDisplayDriver(IStringLocalizer<ValuesFieldDisplayDriver> localizer)
        {
            T = localizer;
        }

        #endregion

        #region Driver Methods

        #region Display

        public override IDisplayResult Display(ValuesField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            return Initialize<DisplayValuesFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Field = field;
                model.Part = fieldDisplayContext.ContentPart;
                model.PartFieldDefinition = fieldDisplayContext.PartFieldDefinition;
                model.Data = field.Data;
            })
            .Location("Content")
            .Location("SummaryAdmin", "");
        }

        #endregion

        #region Edit

        public override IDisplayResult Edit(ValuesField field, BuildFieldEditorContext context)
        {
            return Initialize<EditValuesFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;

                model.Data = JConvert.SerializeObject(field.Data);
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ValuesField field, UpdateFieldEditorContext context)
        {
            var model = new EditValuesFieldViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Data);

            field.Data = JConvert.DeserializeObject<List<string>>(model.Data);

            return Edit(field, context);
        }

        #endregion

        #endregion
    }
}
