using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Swenson_He.App_Start
{
    public class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.Remove(formatters.XmlFormatter);
            formatters.Remove(formatters.JsonFormatter);
            var formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DefaultValueHandling = DefaultValueHandling.Populate
                },
                Indent = Boolean.Parse(ConfigurationManager.AppSettings["JsonIndent"])
            };
            formatter.SerializerSettings.Converters.Add(new ExpandoObjectConverter());
            formatters.Insert(0, formatter);
        }
    }
}