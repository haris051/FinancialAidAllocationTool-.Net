#pragma checksum "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e128857dfc67f37c09709a5dde3450fb580c4cf5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_PreviousRecord), @"mvc.1.0.view", @"/Views/Home/PreviousRecord.cshtml")]
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
#line 1 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e128857dfc67f37c09709a5dde3450fb580c4cf5", @"/Views/Home/PreviousRecord.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_PreviousRecord : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<FinancialAidAllocationTool.Models.FaatScholarLog>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "NeedBasedList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
  
    Layout="~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<section style=\"margin-top: 60px;margin-bottom:80px;\"> <h3 style=\"color: white;\">Previous Records</h3> \n<div class=\"card d-xl-block d-lg-block d-none\"");
            BeginWriteAttribute("style", " style=\"", 269, "\"", 277, 0);
            EndWriteAttribute();
            WriteLiteral(@">

<ul class=""list-group"">
    <li class=""list-group-item bg-info px-1"">

<div class=""row"">
    <div class=""col-lg-2 col-md-2 col-2"">
        Class 
    </div>
    <div class=""col-lg-2"">
      Semester
    </div>
    <div class=""col-lg-2 col-md-3 col-3"">
        Type
    </div>
    <div class=""col-lg-1 col-md-2 col-2"">
       Status
    </div>
    <div class=""col-lg-2 col-md-2 col-2"">
       Recommendations  
    </div>
    <div class=""col-lg-3 col-md-3 col-3"">
      Allocation Amount
    </div>
</div>

</li>
");
#nullable restore
#line 34 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
 foreach (var item in Model)
{
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<li class=\"list-group-item px-1\">\n<div class=\"row\">\n    <div class=\"col-lg-2 col-md-2 col-2\">\n        ");
#nullable restore
#line 40 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
   Write(item.CD.Discipline);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 40 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                       Write(item.CD.SemesterCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 40 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                                              Write(item.CD.Section);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <div class=\"col-lg-2\">\n            ");
#nullable restore
#line 43 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
       Write(item.CD.Semester);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <div class=\"col-lg-2 col-md-3 col-3\">\n        ");
#nullable restore
#line 46 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
   Write(item.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n    <div class=\"col-lg-1 col-md-2 col-2\">\n       ");
#nullable restore
#line 49 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
  Write(item.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n     <div class=\"col-lg-2 col-md-2 col-2\">\n");
            WriteLiteral("       <button class=\"Recommend btn btn-sm btn-primary\"");
            BeginWriteAttribute("value", " value=", 1437, "", 1475, 3);
#nullable restore
#line 53 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
WriteAttributeValue("", 1444, item.ApplicationId, 1444, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1463, "-", 1463, 1, true);
#nullable restore
#line 53 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
WriteAttributeValue("", 1464, item.CD.Id, 1464, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Recommendation</button>\n    </div>\n    <div class=\"col-lg-3 col-md-3 col-3\">\n      ");
#nullable restore
#line 56 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
 Write(item.AllocationAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n</div>\n</li>\n");
#nullable restore
#line 60 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>\n</div>\n");
#nullable restore
#line 63 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
 foreach (var item in Model)
{
    


#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card d-xl-none d-lg-none d-block\" style=\"margin-bottom: 6px;\">\n      <div class=\"card-header\">\n           <h4>");
#nullable restore
#line 69 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
          Write(item.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n      </div>\n      <div class=\"card-body\">\n        <div class=\"row\">\n                <div class=\"col-4\">\n                   <label>Type</label>\n                </div>\n                <div class=\"col-8\">\n                   <label>");
#nullable restore
#line 77 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                     Write(item.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                <div class=\"col-4\">\n                   <label>Class</label>\n                </div>\n                <div class=\"col-8\">\n                   <label>");
#nullable restore
#line 83 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                     Write(item.CD.Discipline);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ");
#nullable restore
#line 83 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                                          Write(item.CD.SemesterCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("  ");
#nullable restore
#line 83 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                                                                  Write(item.CD.Section);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                <div class=\"col-4\">\n                      <label>Semester</label>\n                </div>\n                <div class=\"col-8\">\n                      <label>");
#nullable restore
#line 89 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                        Write(item.CD.Semester);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                \n               \n\n                <div class=\"col-4\">\n                   <label>Amount</label>\n                </div>\n                <div class=\"col-8\">\n                   <label>");
#nullable restore
#line 98 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
                     Write(item.AllocationAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                </div>\n                \n        </div>\n\n      </div>\n      <div class=\"card-footer\">\n          <button class=\"Recommend btn btn-sm btn-primary\"");
            BeginWriteAttribute("value", " value=", 2928, "", 2966, 3);
#nullable restore
#line 105 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
WriteAttributeValue("", 2935, item.ApplicationId, 2935, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2954, "-", 2954, 1, true);
#nullable restore
#line 105 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
WriteAttributeValue("", 2955, item.CD.Id, 2955, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Recommendation</button>\n\n      </div>\n</div>\n");
#nullable restore
#line 109 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"

}

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e128857dfc67f37c09709a5dde3450fb580c4cf513149", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "value", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 111 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/PreviousRecord.cshtml"
AddHtmlAttributeValue("", 3079, Model.ToList()[0].CD.Discipline, 3079, 32, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
      


<div class=""modal"" tabindex=""-1"" role=""dialog"" id=""modal"">
  <div class=""modal-dialog modal-dialog-centered modal-dialog-scrollable"" role=""document"">
    <div class=""modal-content"">
      <div class=""modal-header"">
        <h5 class=""modal-title"">Recommendations</h5>
        <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
          <span aria-hidden=""true"">&times;</span>
        </button>
      </div>
      <div class=""modal-body"">
           </div>
      <div class=""modal-footer"">
        <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"" id=""closbtn"">Close</button>
      </div>
    </div>
  </div>
</div>
</section>
<script>
    $("".Recommend"").click(function () {
       //console.log(""Modal"");
       var val = $(this).attr(""value"");
       console.log(val);
        $.ajax({
            
            url: ""/Home/History/"",
            type: ""GET"",
             data: { ""id"":val },                
                success: function (result) {
             ");
            WriteLiteral(@"     //console.log(result);
                  $('.modal-body').empty();        
                  $('.modal-body').append(result);  
                    
                    $('#modal').modal('show');                   
                
            },
            error: function() {  
                    alert(""Content load failed."");  
                }  
        });
        return false;
    });
     $(""#closbtn"").click(function()  
        {  
             
            $('#modal').modal('hide');  
        }); 
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<FinancialAidAllocationTool.Models.FaatScholarLog>> Html { get; private set; }
    }
}
#pragma warning restore 1591
