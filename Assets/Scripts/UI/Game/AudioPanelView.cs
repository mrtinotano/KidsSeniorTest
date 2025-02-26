using KidsTest.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace KidsTest
{
    public class AudioPanelView : MonoBehaviour
    {
        [Header("AudioButton")]
        [SerializeField] private UIButton m_AudioButton;
        [SerializeField] private GameObject m_PlayIcon;
        [SerializeField] private GameObject m_PauseIcon;

        [Space]
        [SerializeField] private Slider m_AudioSlider;

        [Space]
        [SerializeField] private GameObject m_FinishedPopup;

        private AudioPanelViewModel m_ViewModel;
        private AudioPanelViewModel.AudioState m_AudioState = AudioPanelViewModel.AudioState.Stopped;

        private void Awake()
        {
            m_ViewModel = new AudioPanelViewModel();
            m_ViewModel.OnAudioSliderMaxValueChanged += SetAudioSliderMaxValue;
            m_ViewModel.OnAudioSliderValueChanged += SetAudioSliderValue;
            m_ViewModel.OnAudioStateChanged += SetAudioState;
            m_ViewModel.OnAudioFinished += ShowFinishedPopup;

            m_AudioButton.onClick.AddListener(()=> AudioButtonPressed());

            EventManager<SetPlayingAudioSourceEvent>.AddListener(SetPlayingAudioSource);
        }

        private void OnDestroy()
        {
            EventManager<SetPlayingAudioSourceEvent>.RemoveListener(SetPlayingAudioSource);
        }

        private void SetPlayingAudioSource(SetPlayingAudioSourceEvent eventData)
        {
            m_ViewModel.SetPlayingAudioSource(eventData.AudioSource);
        }

        private void SetAudioSliderMaxValue(float value) => m_AudioSlider.maxValue = value;
        private void SetAudioSliderValue(float value) => m_AudioSlider.value = value;
        private void SetAudioState(AudioPanelViewModel.AudioState state) => m_AudioState = state;

        private void Update()
        {
            if (m_AudioState != AudioPanelViewModel.AudioState.Playing)
                return;

            m_ViewModel.UpdateAudioValue();
        }

        private void AudioButtonPressed()
        {
            switch (m_AudioState)
            {
                case AudioPanelViewModel.AudioState.Stopped:
                    PlayAudio(); 
                    break;
                case AudioPanelViewModel.AudioState.Playing:
                    PauseAudio();
                    break;
                case AudioPanelViewModel.AudioState.Paused:
                    UnPauseAudio();
                    break;
            }
        }

        private void PlayAudio()
        {
            m_ViewModel.PlayAudio();
            m_PlayIcon.SetActive(false);
            m_PauseIcon.SetActive(true);
        }

        private void PauseAudio()
        {
            m_ViewModel.PauseAudio();
            m_PlayIcon.SetActive(true);
            m_PauseIcon.SetActive(false);
        }

        private void UnPauseAudio()
        {
            m_ViewModel.UnPauseAudio();
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
