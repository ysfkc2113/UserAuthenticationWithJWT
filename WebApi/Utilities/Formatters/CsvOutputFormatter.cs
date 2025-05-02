using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if(typeof(EventDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<EventDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        private static void FormatCsv(StringBuilder buffer, EventDto eventDto)
        {
            buffer.AppendLine($"{eventDto.Id}, {eventDto.Title}, {eventDto.IsApproved},{eventDto.EventDate},{eventDto.Location}");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, 
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if(context.Object is IEnumerable<EventDto>)
            {
                foreach(var evnt in (IEnumerable<EventDto>)context.Object)
                {
                    FormatCsv(buffer, evnt);
                }
            }
            else
            {
                FormatCsv(buffer, (EventDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
    }
}
