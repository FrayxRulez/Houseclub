using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public class InvitesViewModel : ViewModelBase
    {
        public InvitesViewModel(IDataService dataService)
            : base(dataService)
        {
            Items = new ObservableCollection<object>();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var store = await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);
            if (store == null)
            {
                return;
            }

            var contacts = await store.FindContactsAsync();
            var importedPhones = new Dictionary<string, Contact>();

            foreach (var contact in contacts)
            {
                foreach (var phone in contact.Phones)
                {
                    var phoneNumber = phone.Number.Replace(" ", string.Empty);
                    importedPhones[phoneNumber] = contact;
                }
            }

            var importingContacts = new List<Models.Contact>();

            foreach (var phone in importedPhones.Keys.ToList())
            {
                var contact = importedPhones[phone];
                var firstName = contact.FirstName ?? string.Empty;
                var lastName = contact.LastName ?? string.Empty;

                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                {
                    if (string.IsNullOrEmpty(contact.DisplayName))
                    {
                        continue;
                    }

                    firstName = contact.DisplayName;
                }

                if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                {
                    var item = new Models.Contact
                    {
                        PhoneNumber = phone,
                        Name = $"{firstName} {lastName}".Trim()
                    };

                    importingContacts.Add(item);
                }
            }

            var response = await DataService.SendAsync(new GetSuggestedInvites(importingContacts));
        }

        public ObservableCollection<object> Items { get; private set; }
    }
}
