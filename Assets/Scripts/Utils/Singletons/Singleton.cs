using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KidsTest.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = FindAnyObjectByType<T>();

                return m_Instance;
            }
        }
                
        protected virtual void Awake()
        {
            if (m_Instance == null)
                m_Instance = this as T;
        }
    }
}
