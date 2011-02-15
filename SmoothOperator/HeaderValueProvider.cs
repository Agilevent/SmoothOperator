using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmoothOperator
{
    public class HeaderValueProviderFactory : ValueProviderFactory
    {
        public class HeaderValueProvider : IValueProvider
        {
            private readonly NameValueCollection _headers;

            public HeaderValueProvider(NameValueCollection headers)
            {
                _headers = headers;
            }

            public bool ContainsPrefix(string prefix)
            {
                return _headers.AllKeys.Any(x => x.Contains(prefix)); 
            }

            public ValueProviderResult GetValue(string key)
            {
                if (_headers == null)
                {
                    return null;
                }
                
                var val = _headers.GetValues(key) == null ? null : _headers.GetValues(key).FirstOrDefault();
                return val != null
                           ? new ValueProviderResult(val, val, CultureInfo.CurrentCulture)
                           : null;
            }
        }

        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new HeaderValueProvider(controllerContext.HttpContext.Request.Headers); 
        }
    }
    
}