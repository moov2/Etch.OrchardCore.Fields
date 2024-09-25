using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Fields.Colour
{
    public class AdminMenu : AdminNavigationProvider
    {
        public IStringLocalizer S { get; set; }

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        protected override ValueTask BuildAsync(NavigationBuilder builder)
        {
            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                        .Add(S["Colours"], S["Colours"].PrefixPosition(), layers => layers
                        .AddClass("colours").Id("colours")
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.GroupId })
                            .Permission(Permissions.ManageColourSettings)
                            .LocalNav()
                        )));

            return ValueTask.CompletedTask;
        }
    }
}
