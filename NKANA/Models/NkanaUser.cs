using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace NKANA.Models
{
    public class NkanaUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class NkanaRole : IdentityRole<string>
    {

    }
    public class NkanaUserRole : IdentityUserRole<string>
    {

    }
    public class NkanaUserClaim : IdentityUserClaim<string>
    {

    }
    public class NkanaUserToken : IdentityUserToken<string>
    {

    }
    public class NkanaUserLogin : IdentityUserLogin<string>
    {

    }
    public class NkanaRoleClaim : IdentityRoleClaim<string>
    {

    }
    public class NkanaUserManager : UserManager<NkanaUser>
    {
        public NkanaUserManager(IUserStore<NkanaUser> userStore, IOptions<IdentityOptions> options, IPasswordHasher<NkanaUser> passwordHasher,
                IEnumerable<IUserValidator<NkanaUser>> userValidator, IEnumerable<IPasswordValidator<NkanaUser>> passwordValidator,
                ILookupNormalizer lookupNormalizer, IdentityErrorDescriber errorDescriber, ILogger<NkanaUserManager> logger, IServiceProvider services):
            base(userStore, options, passwordHasher, userValidator, passwordValidator, lookupNormalizer, errorDescriber, services, logger)
        {

        }
    }
    public class NkanaRoleManager : RoleManager<NkanaRole>
    {
        public NkanaRoleManager(IRoleStore<NkanaRole> roleStore, IEnumerable<IRoleValidator<NkanaRole>> roleValidator,
                ILookupNormalizer lookupNormalizer, IdentityErrorDescriber errorDescriber, ILogger<RoleManager<NkanaRole>> logger) :
            base(roleStore,roleValidator, lookupNormalizer,errorDescriber,logger)
        {

        }
    }
    public class NkanaSignInManager : SignInManager<NkanaUser>
    {
        public NkanaSignInManager(NkanaUserManager userManager, IHttpContextAccessor contextAccessor,
                IUserClaimsPrincipalFactory<NkanaUser> claimsFactory, IOptions<IdentityOptions> options, ILogger<NkanaSignInManager> logger,
            IAuthenticationSchemeProvider schemesProvider, IUserConfirmation<NkanaUser> userConfirmation) :
            base(userManager, contextAccessor, claimsFactory, options, logger, schemesProvider, userConfirmation)
        {

        }
    }
}
