using KidsTest.Utils;

namespace KidsTest
{
    public class LobbyInterfaceButtonsViewModel
    {
        public void OnPlaySubmit()
        {
            EventManager<LoadGameSceneEvent>.TriggerEvent(new LoadGameSceneEvent());
        }
    }
}
