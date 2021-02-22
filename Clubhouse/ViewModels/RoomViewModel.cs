using Clubhouse.Navigation;
using Clubhouse.Services;

namespace Clubhouse.ViewModels
{
    public class RoomViewModel : ViewModelBase
    {
        private readonly IVoiceService _voiceService;

        public RoomViewModel(ClubhouseAPIController dataService, IVoiceService voiceService)
            : base(dataService)
        {
            _voiceService = voiceService;
        }
    }
}
