using KidsTest.Utils;

namespace KidsTest
{
    public class FinishedAudioPopUpViewViewModel
    {
        public void OnReturnButtonSubmit()
        {
            EventManager<ReturnToLobbyEvent>.TriggerEvent(new ReturnToLobbyEvent());
        }
    }
}

