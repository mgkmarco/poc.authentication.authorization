#pragma checksum "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\Account\ForgotPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4d66b3341bcc6e7e5be8926e8f3e5a6f6fec247c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ForgotPassword), @"mvc.1.0.view", @"/Views/Account/ForgotPassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/ForgotPassword.cshtml", typeof(AspNetCore.Views_Account_ForgotPassword))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\_ViewImports.cshtml"
using CustomIdentityProviderSample;

#line default
#line hidden
#line 2 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\_ViewImports.cshtml"
using CustomIdentityProviderSample.Models;

#line default
#line hidden
#line 3 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\_ViewImports.cshtml"
using CustomIdentityProviderSample.Models.AccountViewModels;

#line default
#line hidden
#line 4 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\_ViewImports.cshtml"
using CustomIdentityProviderSample.Models.ManageViewModels;

#line default
#line hidden
#line 5 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d66b3341bcc6e7e5be8926e8f3e5a6f6fec247c", @"/Views/Account/ForgotPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40ddaa8d9431d5d207cdfc24d3ac1a44c99cde52", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ForgotPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ForgotPasswordViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\Account\ForgotPassword.cshtml"
  
    ViewData["Title"] = "Forgot your password?";

#line default
#line hidden
            BeginContext(89, 6, true);
            WriteLiteral("\r\n<h2>");
            EndContext();
            BeginContext(96, 17, false);
#line 6 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\Account\ForgotPassword.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(113, 164, true);
            WriteLiteral("</h2>\r\n<p>\r\n    For more information on how to enable reset password please see this <a href=\"https://go.microsoft.com/fwlink/?LinkID=532713\">article</a>.\r\n</p>\r\n\r\n");
            EndContext();
            BeginContext(978, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(998, 2, true);
                WriteLiteral("\r\n");
                EndContext();
#line 30 "C:\repos\poc-authandauth\src\poc.authentication.portal\Views\Account\ForgotPassword.cshtml"
       await Html.RenderPartialAsync("_ValidationScriptsPartial"); 

#line default
#line hidden
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ForgotPasswordViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
