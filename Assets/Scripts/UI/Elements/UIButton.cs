using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using KidsTest.Utils;

namespace KidsTest
{
    public class UIButton : Button
    {
        [Header("Button Press")]
        [SerializeField] private Vector3 m_PressPunch;
        [SerializeField] private float m_PunchTime;
        [SerializeField] private int m_PunchVibrato = 1;
        [SerializeField] private int m_PunchElasticity = 1;

        [Header("Button Audio")]
        [SerializeField] private AudioClip m_ButtonSound;

        public Action OnButtonPressed;

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
            transform.DOKill(true);
            transform.DOPunchScale(m_PressPunch, m_PunchTime, m_PunchVibrato, m_PunchElasticity);
        }
    }
}
