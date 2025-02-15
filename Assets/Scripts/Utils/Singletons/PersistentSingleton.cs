using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest.Utils
{
    public class PersistentSingleton<T> : Singleton<T> where T : Component
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
