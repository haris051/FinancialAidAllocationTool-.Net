#pragma checksum "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aceb5cb0182dfdf5430db37bb3ac2165aae7173d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MeritList), @"mvc.1.0.view", @"/Views/Home/MeritList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/MeritList.cshtml", typeof(AspNetCore.Views_Home_MeritList))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool;

#line default
#line hidden
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool.Models;

#line default
#line hidden
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
using System.Collections.Generic;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aceb5cb0182dfdf5430db37bb3ac2165aae7173d", @"/Views/Home/MeritList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MeritList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<FinancialAidAllocationTool.Models.ClassDetails>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(60, 2, true);
            WriteLiteral("  ");
            EndContext();
#line 3 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
  
Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(144, 118, true);
            WriteLiteral("\n     <div style=\"margin-top: 20px;\">\n            \n                <section class=\"Boxes\"> \n\n             \n          \n");
            EndContext();
            BeginContext(280, 275, true);
            WriteLiteral(@"                 <div class=""row"">
                   <div class=""col-sm-12"">
                 <button  name=""CompleteList"" class=""btn btn-outline-primary SelectAll""  value=""Accept"" onclick=""SubmitForms()"">SelectAll</button>
                    </div>
                </div>
");
            EndContext();
#line 19 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                
                   var count =0 ;
                  
                  

#line default
#line hidden
#line 22 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                   foreach(var obj in Model)
                  {  


                              



#line default
#line hidden
            BeginContext(727, 171, true);
            WriteLiteral("                                <div class=\"table-responsive\">\n                               <table class=\"table table-sm MyTable\">\n                                   <tr");
            EndContext();
            BeginWriteAttribute("id", " id=", 898, "", 908, 1);
#line 31 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 902, count, 902, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(908, 161, true);
            WriteLiteral(" onclick=\"ToggleMenu(this)\" style=\"color: white;\">\n                                       <td class=\"MyTableTd\">\n                                         <h5><u>");
            EndContext();
            BeginContext(1070, 14, false);
#line 33 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                           Write(obj.Discipline);

#line default
#line hidden
            EndContext();
            BeginContext(1084, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1086, 17, false);
#line 33 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                                           Write(obj.SemesterCount);

#line default
#line hidden
            EndContext();
            BeginContext(1103, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1105, 11, false);
#line 33 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                                                              Write(obj.Section);

#line default
#line hidden
            EndContext();
            BeginContext(1116, 221, true);
            WriteLiteral("</u></h5>\n\n                                       </td>\n                                       <td class=\"MyTableTd\" style=\"text-align:right\" colspan=\"6\"> \n                                         <h5><u>Total Strength : ");
            EndContext();
            BeginContext(1338, 17, false);
#line 37 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                                            Write(obj.ClassStrength);

#line default
#line hidden
            EndContext();
            BeginContext(1355, 209, true);
            WriteLiteral("</u></h5>\n\n                                       </td>\n                                   </tr>\n                               \n                                </table>\n                                </div>\n");
            EndContext();
#line 44 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                       foreach(var i in obj.StudentList)
                        
                          {
                            using(Html.BeginForm("MeritList", "Home",FormMethod.Post))
                            { 

#line default
#line hidden
            BeginContext(1792, 214, true);
            WriteLiteral("                         <div class=\"table-responsive\">\n\n                          <table class=\"table table-sm table-borderless table-hover MyTable\" style=\"table-layout:fixed\" >\n                                <tr");
            EndContext();
            BeginWriteAttribute("id", " id=", 2006, "", 2016, 1);
#line 52 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 2010, count, 2010, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 2016, "\"", 2039, 2);
            WriteAttributeValue("", 2024, "collapse", 2024, 8, true);
#line 52 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue(" ", 2032, count, 2033, 6, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2040, 103, true);
            WriteLiteral(">\n                                    <td style=\"display:none\">\n                                       ");
            EndContext();
            BeginContext(2144, 73, false);
#line 54 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                  Write(Html.TextBox("Discipline",obj.Discipline,new{@style="border: 0px none;"}));

#line default
#line hidden
            EndContext();
            BeginContext(2217, 144, true);
            WriteLiteral("\n\n                                    </td>\n                                    <td style=\"display:none\">\n                                      ");
            EndContext();
            BeginContext(2362, 64, false);
#line 58 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                 Write(Html.Hidden("SemesterNo",obj.SemesterNo,new{@style="width:0px"}));

#line default
#line hidden
            EndContext();
            BeginContext(2426, 307, true);
            WriteLiteral(@"

                                    </td>
                                   
                                   
                 
                                    <td style=""vertical-align: middle; color:white"" colspan=""2"" class=""MyTableTd"">
                                      <input type=""hidden""");
            EndContext();
            BeginWriteAttribute("value", " value=", 2733, "", 2753, 1);
#line 65 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 2740, i.RegisterNo, 2740, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2753, 106, true);
            WriteLiteral(" name=\"RegistrationNo\" style=\"border: 0px none;\" readonly></input>\n                                       ");
            EndContext();
            BeginContext(2860, 12, false);
#line 66 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                  Write(i.RegisterNo);

#line default
#line hidden
            EndContext();
            BeginContext(2872, 215, true);
            WriteLiteral("\n                                    </td>\n                                     <td colspan=\"2\" style=\"vertical-align: middle;text-align:center;color:white\" class=\"MyTableTd\">\n                                       ");
            EndContext();
            BeginContext(3088, 6, false);
#line 69 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                  Write(i.CGPA);

#line default
#line hidden
            EndContext();
            BeginContext(3094, 59, true);
            WriteLiteral("\n                                      <input type=\"hidden\"");
            EndContext();
            BeginWriteAttribute("value", " value=", 3153, "", 3167, 1);
#line 70 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 3160, i.CGPA, 3160, 7, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3167, 276, true);
            WriteLiteral(@" name=""CGPA"" style=""border: 0px none;text-align:center;border:none;"" readonly></input>
                                     </td>
                                     <td style=""padding: 0;vertical-align:middle;"" class=""MyTableTd"">
                                      <input");
            EndContext();
            BeginWriteAttribute("value", " value=", 3443, "", 3469, 1);
#line 73 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 3450, i.AllocationAmount, 3450, 19, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3469, 216, true);
            WriteLiteral(" name=\"AllocationAmount\" style=\"width:100%;\"></input>  \n\n                                    </td>\n                                      <td>\n                                       <button type=\"submit\" name=\"Submit\"");
            EndContext();
            BeginWriteAttribute("id", " id=", 3685, "", 3702, 1);
#line 77 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
WriteAttributeValue("", 3689, i.RegisterNo, 3689, 13, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(3702, 451, true);
            WriteLiteral(@" value=""Accept"" class=""btn btn-outline-success btn-sm"">Accept</button>

                                    </td>
                                      <td>
                                       <button type=""submit"" name=""Submit"" value=""Reject"" class=""btn btn-outline-danger btn-sm"">Reject</button>

                                    </td>
                                </tr>


                          </table>
                         </div>
");
            EndContext();
#line 89 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                                
                            }
                            
                      
                      
                      }

#line default
#line hidden
#line 94 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                       
                      count++;
                      }

#line default
#line hidden
            BeginContext(4426, 30, true);
            WriteLiteral("                </section>   \n");
            EndContext();
            BeginContext(4488, 1763, true);
            WriteLiteral(@"                  <script type=""text/javascript"">
                              function SubmitForms()
                              { 

                                
                              
                             
                                var AllocatedAmountLength=document.getElementsByName(""AllocationAmount"").length;
                                var Amount = [];
                                for (i = 0; i < AllocatedAmountLength; i++) { 
                                  //Push each element to the array
                                  Amount.push(document.getElementsByName(""AllocationAmount"")[i].value);
                                }


                                var RegistrationNoLength=document.getElementsByName(""RegistrationNo"").length;
                                var RegisterNo = [];
                                for (i = 0; i < RegistrationNoLength; i++) { 
                                  //Push each element to the array
                                  Reg");
            WriteLiteral(@"isterNo.push(document.getElementsByName(""RegistrationNo"")[i].value);
                                }

                                
                               
                               
                                                       $.post(""/Home/CompleteList"",
                                                        {
                                                          AllocatedAmount: Amount,
                                                          RegistrationNo : RegisterNo,
                                                          Status : ""Accept"",
                                                          Type : ""MeritList"",
                                                          Discipline: '");
            EndContext();
            BeginContext(6252, 19, false);
#line 134 "/home/muhammad/Projects/FinancialAidAllocationTool (19-April-2020) /FinancialAidAllocationTool/Views/Home/MeritList.cshtml"
                                                                  Write(Model[1].Discipline);

#line default
#line hidden
            EndContext();
            BeginContext(6271, 777, true);
            WriteLiteral(@"'
                                                        },
                                                        function(data, status){
                                                         // alert(""Data: "" + data + ""\nStatus: "" + status);
                                                        });
                           
                              return true;
                              }

                              
                              function ToggleMenu(x)
                              {   
                                 
                                  $("".""+x.id).toggleClass(""collapse"");
                                  
                                  
                              }
                            </script>
");
            EndContext();
            BeginContext(7065, 287, true);
            WriteLiteral(@"



               
                            
                       
                          
             
                      
                       
                        
                       
              
         
               
               

                


");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<FinancialAidAllocationTool.Models.ClassDetails>> Html { get; private set; }
    }
}
#pragma warning restore 1591
