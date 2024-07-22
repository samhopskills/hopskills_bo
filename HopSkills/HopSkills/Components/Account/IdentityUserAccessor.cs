using HopSkills.Data;
using Microsoft.AspNetCore.Identity;

namespace HopSkills.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<HopSkillsUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<HopSkillsUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
