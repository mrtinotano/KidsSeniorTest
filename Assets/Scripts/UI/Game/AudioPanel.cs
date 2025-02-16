using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class AudioPanel : MonoBehaviour
    {
        [Header("AudioButton")]
        [SerializeField] private Button m_AudioButton;
        [SerializeField] private GameObject m_PlayIcon;
        [SerializeField] private GameObject m_PauseIcon;

        [Space]
        [SerializeField] private Slider m_AudioSlider;

        [Space]
        [SerializeField] private GameObject m_FinishedPopup;

        AudioLevelManager.AudioState m_AudioState => AudioLevelManager.Instance.CurrentAudioState;

        private void Awake()
        {
            m_AudioButton.onClick.AddListener(()=> AudioButtonPressed());
        }

        private void Update()
        {
            if (m_AudioState != AudioLevelManager.AudioState.Playing)
                return;

            m_AudioSlider.value = AudioLevelManager.Instance.AudioTime;

            if (m_AudioSlider.value == m_AudioSlider.maxValue)
                ShowFinishedPopup();
        }

        private void AudioButtonPressed()
        {
            switch (m_AudioState)
            {
                case AudioLevelManager.AudioState.Stopped:
                    PlayAudio(); 
                    break;
                case AudioLevelManager.AudioState.Playing:
                    PauseAudio();
                    break;
                case AudioLevelManager.AudioState.Paused:
                    UnPauseAudio();
                    break;
            }
        }

        private void PlayAudio()
        {
            AudioClip clip = AudioLevelManager.Instance.PlayAudio();
            m_AudioSlider.maxValue = clip.length;
            m_AudioSlider.value = 0;
            m_PlayIcon.SetActive(false);
            m_PauseIcon.SetActive(true);
        }

        private void PauseAudio()
        {
            AudioLevelManager.Instance.PauseAudio();
            m_PlayIcon.SetActive(true);
            m_PauseIcon.SetActive(false);
        }

        private void UnPauseAudio()
        {
            AudioLevelManager.Instance.UnPauseAudio();
            m_PlayIcon.SetActive(false);
            m_PauseIcon.SetActive(true);
        }

        private void ShowFinishedPopup()
        {
            m_FinishedPopup.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
