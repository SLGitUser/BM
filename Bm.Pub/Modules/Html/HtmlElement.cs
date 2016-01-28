using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bm.Modules.Helper;

namespace Bm.Modules.Html
{
    public class HtmlElement
    {
        private readonly string _tag;

        private readonly IDictionary<string, string> _attrs = new Dictionary<string, string>();

        private readonly List<HtmlElement> _children = new List<HtmlElement>();

        private string _innerHTML;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tag"></param>
        public HtmlElement(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            _tag = tag.ToLower();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="text"></param>
        public HtmlElement(string tag, string text) : this(tag)
        {
            Text(text);
        }

        /// <summary>
        /// 设置文本内容
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement Text(string value)
        {
            _children.Add(new HtmlElement("text").Attr("value", value));
            return this;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string Attr(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            string value;
            return _attrs.TryGetValue(name, out value) ? value : null;
        }

        /// <summary>
        /// 设置属性内容
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement Attr(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) return this;

            if (_attrs.ContainsKey(name))
            {
                _attrs[name] = value;
            }
            else
            {
                _attrs.Add(name, value);
            }
            return this;
        }

        /// <summary>
        /// 设置属性内容
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement AttrIf(bool condition, string name, string value)
        {
            return !condition ? this : Attr(name, value);
        }

        /// <summary>
        /// 设置属性内容
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement AttrUnless(bool condition, string name, string value)
        {
            return condition ? this : Attr(name, value);
        }

        /// <summary>
        /// 设置属性内容
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement AttrUnlessNullOrEmpty(string name, string value)
        {
            return string.IsNullOrWhiteSpace(value) ? this : Attr(name, value);
        }

        /// <summary>
        /// 属性内容追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HtmlElement AttrPlus(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name)) return this;

            if (_attrs.ContainsKey(name))
            {
                _attrs[name] = string.Concat(_attrs[name], value);
            }
            else
            {
                _attrs.Add(name, value);
            }
            return this;
        }

        /// <summary>
        /// 增加子元素
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        public HtmlElement Append(params HtmlElement[] children)
        {
            return Append(children.ToList());
        }

        /// <summary>
        /// 增加子元素
        /// </summary>
        /// <param name="children"></param>
        /// <returns></returns>
        public HtmlElement Append(IList<HtmlElement> children)
        {
            if (IsSelfClose())
            {
                throw new ArgumentException("该元素不能增加子元素", nameof(children));
            }
            if (children.IsNullOrEmpty()) return this;
            _children.AddRange(children);
            _innerHTML = null;
            return this;
        }

        public HtmlElement Html(string html)
        {
            _innerHTML = html;
            _children.Clear();
            return this;
        }

        public void Output(StringBuilder builder)
        {
            if ("text".Equals(_tag))
            {
                string value;
                builder.Append(_attrs.TryGetValue("value", out value) ? value : string.Empty);
                return;
            }

            builder.Append("<");
            builder.Append(_tag);
            if (_attrs.Any())
            {
                foreach (var attr in _attrs)
                {
                    builder.AppendFormat(@" {0}=""{1}""", attr.Key, attr.Value);
                }
            }
            if (IsSelfClose())
            {
                builder.Append(" />");
            }
            else
            {
                builder.Append(" >");

                if (string.IsNullOrEmpty(_innerHTML))
                {
                    foreach (var child in _children)
                    {
                        child.Output(builder);
                    }
                }
                else {
                    builder.Append(_innerHTML);
                }
                builder.Append("</");
                builder.Append(_tag);
                builder.Append(">");
            }
        }

        /// <summary>
        /// 转换为HTML代码
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            Output(builder);
            return builder.ToString();
        }

        private bool IsSelfClose()
        {
            return new[] { "br", "hr", "meta", "input" }.Contains(_tag);
        }
    }
}
