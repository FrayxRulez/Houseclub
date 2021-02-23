using Clubhouse.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Clubhouse.Services.Methods
{
    public class GetSuggestedInvites : ClubhouseAPIRequest<BaseResponse>
    {
        public GetSuggestedInvites(List<Contact> contacts)
            : base(HttpMethod.Post, "get_suggested_invites")
        {
            Content = new Body(null, true, contacts);
        }

        private class Body
        {
            [JsonPropertyName("club_id")]
            public string ClubId { get; }

            [JsonPropertyName("upload_contacts")]
            public bool UploadContacts { get; }

            [JsonPropertyName("contacts")]
            public List<Contact> Contacts { get; set; }
            
            public Body(string clubId, bool uploadContacts, List<Contact> contacts)
            {
                ClubId = clubId;
                UploadContacts = uploadContacts;
                Contacts = contacts;
            }
        }
    }
}
