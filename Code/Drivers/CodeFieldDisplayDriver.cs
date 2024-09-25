using Etch.OrchardCore.Fields.Code.Fields;
using Etch.OrchardCore.Fields.Code.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Code.Drivers
{
    public class CodeFieldDisplayDriver : ContentFieldDisplayDriver<CodeField>
    {
        #region Implementation

        #region Display

        public override IDisplayResult Display(CodeField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            return Initialize<DisplayCodeFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Field = field;
                model.Part = fieldDisplayContext.ContentPart;
                model.PartFieldDefinition = fieldDisplayContext.PartFieldDefinition;
            })
            .Location("Content")
            .Location("SummaryAdmin", "");
        }

        #endregion Display

        #region Edit

        public override IDisplayResult Edit(CodeField field, BuildFieldEditorContext context)
        {
            return Initialize<EditCodeFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;
                model.Value = field.Value;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(CodeField field, UpdateFieldEditorContext context)
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
