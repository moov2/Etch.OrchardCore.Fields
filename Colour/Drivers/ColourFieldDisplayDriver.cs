using System.Threading.Tasks;
using Etch.OrchardCore.Fields.Code.ViewModels;
using Etch.OrchardCore.Fields.Colour.Fields;
using Etch.OrchardCore.Fields.Colour.Settings;
using Etch.OrchardCore.Fields.Colour.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.Colour.Drivers
{
    public class ColourFieldDisplayDriver : ContentFieldDisplayDriver<ColourField>
    {
        #region Implementation

        #region Edit

        public override IDisplayResult Edit(ColourField field, BuildFieldEditorContext context)
        {
            var settings = context.PartFieldDefinition.GetSettings<ColourFieldSettings>();

            return Initialize<EditColourFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;
                model.Value = string.IsNullOrWhiteSpace(field.Value) ? settings.DefaultValue : field.Value;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(ColourField field, UpdateFieldEditorContext context)
        {
            var model = new EditCodeFieldViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Value))
            {
                field.Value = model.Value;
            }

            return Edit(field, context);
        }

        #endregion Edit

        #endregion Implementation
    }
}
