﻿using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace Web.API.Utilities.Formatters
{
    public class CsvOutputFormatter: TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (typeof(BookDTO).IsAssignableFrom(type) ||
                typeof(IEnumerable<BookDTO>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        private static void FormatCsv(StringBuilder buffer, BookDTO book)
        {
            buffer.AppendLine($"{book.Id}, {book.Title},{book.Price}");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<BookDTO>)
            {
                foreach (var book in (IEnumerable<BookDTO>)context.Object)
                {
                    FormatCsv(buffer, book);
                }
            }
            else
            {
                FormatCsv(buffer, (BookDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString()); 
        }
    }
}
