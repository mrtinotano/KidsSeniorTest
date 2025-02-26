using UnityEngine;

namespace KidsTest.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindAnyObjectByType<T>();
                }

                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            m_Instance = this as T;
        }
    }
}
