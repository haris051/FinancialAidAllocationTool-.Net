#pragma checksum "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9f7697f1ca00f9cf8f3cce55ee03065de25a3d17"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Policy_AddPolicy), @"mvc.1.0.view", @"/Views/Policy/AddPolicy.cshtml")]
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
#nullable restore
#line 1 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\_ViewImports.cshtml"
using FinancialAidAllocationTool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\_ViewImports.cshtml"
using FinancialAidAllocationTool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
using HtmlHelpers.BeginCollectionItemCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9f7697f1ca00f9cf8f3cce55ee03065de25a3d17", @"/Views/Policy/AddPolicy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Policy_AddPolicy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FinancialAidAllocationTool.Models.Policy.FaatPolicy>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-size: 2vh;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "AddRule", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
  
    ViewData["Title"] = "Edit";
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
 using (Html.BeginCollectionItem("FaatPolicy"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""card mt-1 rowPolicy PTHead"">
  
        <div class=""card-header"">
   
        
            <div class=""row P"">                                
                 <div class=""form-group col-lg-2 col-2 text-center"" style=""padding-top: 8px;"">
                    <a href=""#"" class=""float-left addRuleExpand fa fa-plus"" style=""font-size: 2vh;"">
                                        
                    </a>                
                </div>
                <div class=""form-group col-lg-2 col-3 px-0 text-center"">
                 <label class=""switch"">     
                ");
#nullable restore
#line 23 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
           Write(Html.RadioButton("IsSelected","1",new{@class="Radio success",onclick="Radio()"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                <span class=\"slider round\"></span>\r\n        \r\n                </label>\r\n                </div>\r\n               \r\n                <div class=\"form-group col-lg-3 col-2 text-center px-0\" style=\"padding-top: 2px;\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9f7697f1ca00f9cf8f3cce55ee03065de25a3d176861", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 30 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Name);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("                \r\n                </div>\r\n                <div class=\"form-group col-lg-3 col-2 text-center px-0\" style=\"padding-top: 2px;\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9f7697f1ca00f9cf8f3cce55ee03065de25a3d178654", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 33 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.MeritMinCGPA);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"                
                </div>
                <div class=""form-group col-lg-2 col-3 px-0 text-center"" style=""padding-top: 2px;"">
                    <a href=""#"" class=""btn btn-danger delete-policy"" style=""font-size: 2vh;"">
                        <i class=""fa fa-trash"" aria-hidden=""true""></i>
                    </a>
                </div>                
            </div>
            </div>
      
    <section>
        
         <ul class=""list-group collapse RuleURL tbodyRule""> 
             <li class=""list-group-item bg-info"">                     
                <div class=""row"">            
                    <div class=""form-group col-6"" id=""Rule"">
                        <a href=""#"" class=""float-left addRule""");
            BeginWriteAttribute("data", " data =\"", 2203, "\"", 2239, 1);
#nullable restore
#line 49 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
WriteAttributeValue("", 2211, ViewData["ContainerPrefix"], 2211, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                            <i class=""fa fa-plus-square"" aria-hidden=""true""></i>                    
                        </a>
                        <h5 class=""text-white"" >Rule</h5>                       
                    </div>
                </div> 
         
               
                    
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9f7697f1ca00f9cf8f3cce55ee03065de25a3d1711824", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 58 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.FaatRule);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </li>\r\n                    \r\n                                    \r\n");
#nullable restore
#line 62 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
                         foreach (var item in Model.FaatRule)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9f7697f1ca00f9cf8f3cce55ee03065de25a3d1713874", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 64 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => item);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("for", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 65 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    \r\n                    </ul>\r\n               \r\n          \r\n       \r\n    </section>\r\n     \r\n     </div>\r\n");
#nullable restore
#line 74 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Policy\AddPolicy.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FinancialAidAllocationTool.Models.Policy.FaatPolicy> Html { get; private set; }
    }
}
#pragma warning restore 1591
