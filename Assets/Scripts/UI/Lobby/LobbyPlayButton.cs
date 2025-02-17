using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class LobbyPlayButton : Button
    {
        protected override void Awake()
        {
            base.Awake();

            onClick.AddListener(() => CustomizationManager.Instance.LoadGameScene());
        }
    }
}
