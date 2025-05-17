using UnityEngine;

namespace KidsTest.Utils
{
    public class PersistentMonoSingleton<T> : MonoSingleton<T> where T : Component
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
