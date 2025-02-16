using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KidsTest
{
    public class UIButton : Button
    {
        [SerializeField] private Vector3 m_PressPunch;
        [SerializeField] private float m_PunchTime;

        public override void OnSubmit(BaseEventData eventData)
        {
            DoPunch();
            base.OnSubmit(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            DoPunch();
            base.OnPointerClick(eventData);
        }

        private void DoPunch()
        {
            transform.DOPunchScale(m_PressPunch, m_PunchTime, 1);
        }
    }
}
