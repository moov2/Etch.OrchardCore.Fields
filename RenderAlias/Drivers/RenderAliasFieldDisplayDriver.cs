using System.Threading.Tasks;
using Etch.OrchardCore.Fields.RenderAlias.Fields;
using Etch.OrchardCore.Fields.RenderAlias.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;

namespace Etch.OrchardCore.Fields.RenderAlias.Drivers
{
    public class RenderAliasFieldDisplayDriver : ContentFieldDisplayDriver<RenderAliasField>
    {
        #region Overrides

        #region Display

        public override IDisplayResult Display(RenderAliasField field, BuildFieldDisplayContext fieldDisplayContext)
        {
            return Initialize<RenderAliasFieldViewModel>(GetDisplayShapeType(fieldDisplayContext), model =>
            {
                model.Alias = field.Alias;
                model.DisplayType = field.DisplayType;
            })
            .Location("Content")
            .Location("SummaryAdmin", "")
            .Location("DetailAdmin", "");
        }

        #endregion Display

        #region Edit

        public override IDisplayResult Edit(RenderAliasField field, BuildFieldEditorContext context)
        {
            return Initialize<EditRenderAliasFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;

                model.Alias = field.Alias;
                model.DisplayType = field.DisplayType;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(RenderAliasField field, UpdateFieldEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(field, Prefix);

            return Edit(field, context);
        }

        #endregion Edit

        #endregion Overrides
    }
}
