﻿using System;
using System.Text;
using System.Web.Mvc;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.HtmlHelpers
{
    /// <summary>
    /// 分页辅助器
    /// </summary>
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pageinfo, Func<int,string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i < pageinfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");//构造一个a标签
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageinfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}