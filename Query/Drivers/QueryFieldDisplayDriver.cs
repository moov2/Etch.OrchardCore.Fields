using Etch.OrchardCore.Fields.Query.Fields;
using Etch.OrchardCore.Fields.Query.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Queries;
using System.Linq;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Query.Drivers
{
    public class QueryFieldDisplayDriver : ContentFieldDisplayDriver<QueryField>
    {
        #region Dependencies

        private readonly IQueryManager _queryManager;

        #endregion

        #region Constructor

        public QueryFieldDisplayDriver(IQueryManager queryManager)
        {
            _queryManager = queryManager;
        }

        #endregion

        #region Implementation

        #region Edit

        public override async Task<IDisplayResult> EditAsync(QueryField field, BuildFieldEditorContext context)
        {
            var queries = (await _queryManager.ListQueriesAsync()).Select(x => x.Name);

            return Initialize<EditQueryFieldViewModel>(GetEditorShapeType(context), model =>
            {
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;
                model.Value = field.Value;
                model.Queries = queries;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(QueryField field, UpdateFieldEditorContext context)
        {
            var model = new EditQueryFieldViewModel();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.Value))
            {
                field.Value = model.Value;
            }

            return await EditAsync(field, context);
        }

        #endregion Edit

        #endregion Implementation
    }
}
