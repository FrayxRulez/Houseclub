using Clubhouse.Models;
using System.Net.Http;

namespace Clubhouse.Services.Methods
{
    public class GetEvent : ClubhouseAPIRequest<GetEvent.Response>
    {
        public GetEvent(string id)
            : base(HttpMethod.Post, "get_event")
        {
            requestBody = new Body(id);
        }

        private class Body
        {
            public string eventHashid { get; set; }

            public Body(string eventHashid)
            {
                this.eventHashid = eventHashid;
            }
        }

        public class Response
        {
            public Event eventz { get; set; }
        }
    }
}
