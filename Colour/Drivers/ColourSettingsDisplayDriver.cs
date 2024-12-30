using Etch.OrchardCore.Fields.Colour.Settings;
using Etch.OrchardCore.Fields.Colour.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using System.Text.Json;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Colour.Drivers
{
    public class ColourSettingsDisplayDriver : SectionDisplayDriver<ISite, ColourSettings>
    {
        #region Dependencies

        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public ColourSettingsDisplayDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Overrides

        public override IDisplayResult Edit(ISite site, ColourSettings section, BuildEditorContext context)
        {
            return Initialize<ColourSettingsViewModel>("ColourSettings_Edit", model =>
            {
                model.Colours = JConvert.SerializeObject(section.Colours);
            })
            .Location("Content:3")
            .OnGroup(Constants.GroupId)
            .RenderWhen(() => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, Permissions.ManageColourSettings));
        }

        public override async Task<IDisplayResult> UpdateAsync(ISite site, ColourSettings section, UpdateEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageColourSettings))
            {
                return null;
            }

            if (context.GroupId == Constants.GroupId)
            {
                var model = new ColourSettingsViewModel();

                if (await context.Updater.TryUpdateModelAsync(model, Prefix))
                {
                    section.Colours = JConvert.DeserializeObject<ColourItem[]>(model.Colours);
                }
            }

            return Edit(site, section, context);
        }

        #endregion
    }
}
