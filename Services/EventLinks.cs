using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Services.Contracts;
using System.ComponentModel.Design;

namespace Services
{
    public class EventLinks : IEventLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<EventDto> _dataShaper;

        public EventLinks(LinkGenerator linkGenerator, 
            IDataShaper<EventDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<EventDto> eventDto, 
            string fields, 
            HttpContext httpContext)
        {
            var shapedEvents = ShapeData(eventDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedEvents(eventDto, fields, httpContext, shapedEvents);
            return ReturnShapedEvents(shapedEvents);
        }

        private LinkResponse ReturnLinkedEvents(IEnumerable<EventDto> eventDto, 
            string fields, 
            HttpContext httpContext, 
            List<Entity> shapedEvents)
        {
            var eventDtoList = eventDto.ToList();

            for (int index = 0; index < eventDtoList.Count(); index++)
            {
                var eventLinks = CreateForEvent(httpContext, eventDtoList[index], fields);
                shapedEvents[index].Add("Links", eventLinks);
            }

            var eventCollection = new LinkCollectionWrapper<Entity>(shapedEvents);
            CreateForEvents(httpContext,eventCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = eventCollection };
        }

        private LinkCollectionWrapper<Entity> CreateForEvents(HttpContext httpContext, 
            LinkCollectionWrapper<Entity> eventCollectionWrapper)
        {
            eventCollectionWrapper.Links.Add(new Link() 
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel = "self",
                Method = "GET"
            });
            return eventCollectionWrapper;
        }

        private List<Link> CreateForEvent(HttpContext httpContext, 
            EventDto eventDto, 
            string fields)
        {
            var links = new List<Link>()
            {
               new Link()
               { 
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                   $"/{eventDto.Id}",
                   Rel = "self",
                   Method = "GET"
               },
               new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                   Rel="create",
                   Method = "POST"
               },
            };
            return links;
        }

        private LinkResponse ReturnShapedEvents(List<Entity> shapedEvents)
        {
            return new LinkResponse() { ShapedEntities = shapedEvents };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<EventDto> eventsDto, string fields)
        {
            return _dataShaper
                .ShapeData(eventsDto, fields)
                .Select(b => b.Entity)
                .ToList();
        }

       
    }
}
