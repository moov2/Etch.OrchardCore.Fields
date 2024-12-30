using System.Threading.Tasks;
using Etch.OrchardCore.Fields.MultiSelect.Fields;
using Etch.OrchardCore.Fields.MultiSelect.ViewModels;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.MultiSelect.Drivers
{
    public class MultiSelectFieldDisplayDriver : ContentFieldDisplayDriver<MultiSelectField>
    {
        #region Dependencies

        public IStringLocalizer T { get; set; }

        #endregion

        #region Constructor

        public MultiSelectFieldDisplayDriver(IStringLocalizer<MultiSelectFieldDisplayDriver> localizer)
        {
            T = localizer;
        }

        #endregion

        #region Driver Methods

        #region Display

        public override IDisplayResult Display(MultiSelectField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            return Initialize<DisplayMultiSelectFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Field = field;
                model.Part = fieldDisplayContext.ContentPart;
                model.PartFieldDefinition = fieldDisplayContext.PartFieldDefinition;
                model.SelectedValues = field.SelectedValues;
            })
            .Location("Content")
            .Location("SummaryAdmin", "");
        }

        #endregion

        #region Edit

        public override IDisplayResult Edit(MultiSelectField field, BuildFieldEditorContext context)
        {
            return Initialize<EditMultiSelectFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;

                model.SelectedValues = field.SelectedValues;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(MultiSelectField field, UpdateFieldEditorContext context)
        {
            var model = new EditMultiSelectFieldViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.SelectedValues)) {
                field.SelectedValues = model.SelectedValues;
            }

            return Edit(field, context);

        }

        #endregion

        #endregion
    }
}
