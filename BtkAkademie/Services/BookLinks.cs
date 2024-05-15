using Entities.DTOs;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Services.Contracts;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.Net.Http.Headers;

namespace Services
{
    public class BookLinks : IBookLinks
    {
        private readonly IDataShaper<BookDTO> _dataShaper;
        private readonly LinkGenerator _linkGenerator;

        public BookLinks(IDataShaper<BookDTO> dataShaper, LinkGenerator linkGenerator)
        {
            _dataShaper = dataShaper;
            _linkGenerator = linkGenerator;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<BookDTO> booksDto, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapeData(booksDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(booksDto, fields, httpContext, shapedBooks);
            return ReturnShapedBook(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDTO> booksDto, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            var bookDtoList = booksDto.ToList();

            for (int index = 0; index < bookDtoList.Count(); index++)
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[index], fields);
                shapedBooks[index].Add("Links", bookLinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);

            CreateForBook(httpContext, bookCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = bookCollection };
        }

        private LinkCollectionWrapper<Entity> CreateForBook(HttpContext httpContext,LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.Add(new Link()
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel ="self",
                Method ="GET"
            });
            return bookCollectionWrapper;
        }

        private List<Link> CreateForBook(HttpContext httpContext, BookDTO bookDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link()
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["Controller"].ToString().ToLower()}" +
                    $"/{bookDto.Id}",
                    Rel = "self",
                    Method = "GET"
                },
                new Link()
                {
                    Href = $"/api/{httpContext.GetRouteData().Values["Controller"].ToString().ToLower()}",
                    Rel = "create",
                    Method = "POST"
                },
            };
            return links;
        }

        private LinkResponse ReturnShapedBook(List<Entity> shapedBooks)
        {
            return new LinkResponse() { ShapedEntities = shapedBooks };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<BookDTO> booksDto, string fields)
        {
            return _dataShaper.ShapeData(booksDto, fields)
                .Select(b=>b.Entity)
                .ToList();
        }
    }
}
