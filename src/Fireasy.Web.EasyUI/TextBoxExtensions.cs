﻿// -----------------------------------------------------------------------
// <copyright company="Fireasy"
//      email="faib920@126.com"
//      qq="55570729">
//   (c) Copyright Fireasy. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Fireasy.Web.Mvc;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Fireasy.Web.EasyUI
{
    public static class TextBoxExtensions
    {
        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="exp">属性名或使用 txt 作为前缀的 ID 名称。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextBox(this HtmlHelper htmlHelper, string exp, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            var builder = new EasyUITagBuilder("input", "easyui-textbox", settings);
            builder.MergeAttribute("name", exp);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-input");
            builder.GenerateId("txt" + exp);
            return new ExtendHtmlString(builder);
        }

        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <typeparam name="TModel">数据模型类型。</typeparam>
        /// <typeparam name="TProperty">绑定的属性的类型。</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">指定绑定属性的表达式。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var propertyName = metadata.PropertyName;
            settings.Bind(typeof(TModel), propertyName);

            var builder = new EasyUITagBuilder("input", "easyui-textbox", settings);
            builder.MergeAttribute("name", propertyName);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-input");
            builder.GenerateId("txt" + propertyName);
            return new ExtendHtmlString(builder);
        }

        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="exp">属性名或使用 txt 作为前缀的 ID 名称。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextMultiBox(this HtmlHelper htmlHelper, string exp, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            settings.Multiline = true;
            var builder = new EasyUITagBuilder("textarea", "easyui-textbox", settings);
            builder.MergeAttribute("name", exp);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-multi");
            builder.GenerateId("txt" + exp);
            return new ExtendHtmlString(builder);
        }

        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <typeparam name="TModel">数据模型类型。</typeparam>
        /// <typeparam name="TProperty">绑定的属性的类型。</typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">指定绑定属性的表达式。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextMultiBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            settings.Multiline = true;

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var propertyName = metadata.PropertyName;
            settings.Bind(typeof(TModel), propertyName);

            var builder = new EasyUITagBuilder("textarea", "easyui-textbox", settings);
            builder.MergeAttribute("name", propertyName);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-multi");
            builder.GenerateId("txt" + propertyName);
            return new ExtendHtmlString(builder);
        }

        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="exp">属性名或使用 txt 作为前缀的 ID 名称。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextPasswordBox(this HtmlHelper htmlHelper, string exp, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            settings.Type = "password";

            var builder = new EasyUITagBuilder("input", "easyui-textbox", settings);
            builder.MergeAttribute("name", exp);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-input");
            builder.GenerateId("txt" + exp);
            return new ExtendHtmlString(builder);
        }

        /// <summary>
        /// 为 <see cref="HtmlHelper"/> 对象扩展 textbox 元素。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="id">ID 属性值。</param>
        /// <param name="settings">参数选项。</param>
        /// <returns></returns>
        public static ExtendHtmlString TextPasswordBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TextBoxSettings settings = null)
        {
            settings = settings ?? new TextBoxSettings();
            settings.Type = "password";

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var propertyName = metadata.PropertyName;
            settings.Bind(typeof(TModel), propertyName);

            var builder = new EasyUITagBuilder("input", "easyui-textbox", settings);
            builder.MergeAttribute("name", propertyName);
            builder.MergeAttribute("data-options", SettingsSerializer.Serialize(settings));
            builder.AddCssClass("form-input");
            builder.GenerateId("txt" + propertyName);
            return new ExtendHtmlString(builder);
        }
    }
}
