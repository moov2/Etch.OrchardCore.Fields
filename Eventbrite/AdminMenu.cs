using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Eventbrite
{
    [Feature("Etch.OrchardCore.Fields.Eventbrite")]
    public class AdminMenu : AdminNavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        protected override ValueTask BuildAsync(NavigationBuilder builder)
        {

            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Eventbrite"], T["Eventbrite"], layers => layers
                        .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.SettingsGroupId })
                        .Permission(Permissions.ManageEventbriteAPI)
                        .LocalNav()
                    ));

            return ValueTask.CompletedTask;
        }
    }
}
