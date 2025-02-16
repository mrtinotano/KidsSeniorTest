using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class AudioPanel : MonoBehaviour
    {
        [SerializeField] private Slider m_AudioSlider;
        [SerializeField] private Button m_AudioButton;
        [SerializeField] private GameObject m_FinishedPopup;

        private bool m_AudioPlaying;

        private void Awake()
        {
            m_AudioButton.onClick.AddListener(()=> PlayAudio());
        }

        private void Update()
        {
            if (!m_AudioPlaying)
                return;

            m_AudioSlider.value = LevelManager.Instance.AudioTime;

            if (m_AudioSlider.value == m_AudioSlider.maxValue)
            {
                m_AudioPlaying = false;
                ShowFinishedPopup();
            }
        }

        private void PlayAudio()
        {
            AudioClip clip = LevelManager.Instance.PlayAudio();
            m_AudioSlider.maxValue = clip.length;
            m_AudioSlider.value = 0;
            m_AudioPlaying = true;
        }

        private void ShowFinishedPopup()
        {
            m_FinishedPopup.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
