using System;
using System.Collections.Generic;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Globalization;
using System.Threading;

namespace AdSite.TagHelpers
{
    [HtmlTargetElement("options", Attributes = "asp-items")]
    public class LanguageSelectOptionsTagHelper : TagHelper
    {
        [HtmlAttributeNotBound]
        public IHtmlGenerator Generator { get; set; }
        [HtmlAttributeName("asp-items")]
        public object Items { get; set; }

        public LanguageSelectOptionsTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public string Name { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();
            IEnumerable<LanguageSelectViewModel> items = null;
            if (Items != null)
            {
                var enumerable = Items as System.Collections.IEnumerable;

                if (enumerable != null)
                {
                    if (Items is IEnumerable<LanguageSelectViewModel>)
                        items = Items as IEnumerable<LanguageSelectViewModel>;
                    else
                        throw new InvalidOperationException("The {2} was unable to provide metadata about '{1}' expression value '{3}' for <options>.");
                }
                else
                {
                    throw new InvalidOperationException("Invalid items for <options>");
                }

                foreach (var item in items)
                {
                    var selectedAttr = Thread.CurrentThread.CurrentCulture.LCID == item.CultureId ? "selected='selected'" : "";

                    if (!String.IsNullOrEmpty(item.LanguageShortName))
                    {
                        try
                        {
                            var ri = new RegionInfo(new CultureInfo(item.LanguageShortName, false).LCID);

                            //var ri = new RegionInfo(item.LanguageShortName);
                            string twoLetterIsoRegionName = ri?.TwoLetterISORegionName?.ToLower();

                            if (item.LanguageShortName != null)
                                output.Content.AppendHtml($"<option data-content='<span class=\"flag-icon flag-icon-{twoLetterIsoRegionName}\"></span> {item.Text}' value='{item.Value}' {selectedAttr}>{item.Text}</option>");
                            else
                                output.Content.AppendHtml($"<option>{item.Text}</option>");
                        }
                        catch(Exception ex)
                        {
                            
                        }
                    }
                }
            }
        }
    }

}
