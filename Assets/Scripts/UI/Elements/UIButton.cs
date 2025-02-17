using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KidsTest
{
    public class UIButton : Button
    {
        [Header("Button Press")]
        [SerializeField] private Vector3 m_PressPunch;
        [SerializeField] private float m_PunchTime;

        [Header("Button Audio")]
        [SerializeField] private AudioClip m_ButtonSound;

        public override void OnSubmit(BaseEventData eventData)
        {
            OnPressed();
            base.OnSubmit(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            OnPressed();
            base.OnPointerClick(eventData);
        }

        private void OnPressed()
        {
            DoPunch();
            AudioManager.Instance.PlaySound(m_ButtonSound);
        }

        private void DoPunch()
        {
            transform.DOKill();
            transform.DOPunchScale(m_PressPunch, m_PunchTime, 1);
        }
    }
}
