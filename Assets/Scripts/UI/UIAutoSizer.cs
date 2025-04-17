using System.Collections;
using TMPro;
using UnityEngine;

namespace ScreenTest.UI
{
    public class UIAutoSizer : MonoBehaviour
    {
        #region Serialize Fields
        [SerializeField] private TMP_Text m_Text;
        [Space]
        [SerializeField] private float m_DopWeight;
        [SerializeField] private float m_DopHeight;
        [Space]
        [SerializeField] bool m_IsVertical;
        [SerializeField] bool m_IsHorizontal;
        #endregion

        #region Methods
        public void InitSize()
        {
            StartCoroutine(SetSize());
        }

        private IEnumerator SetSize()
        {
            yield return null;

            RectTransform rectTransform = GetComponent<RectTransform>();
            RectTransform textTransform = m_Text.GetComponent<RectTransform>();

            float width;
            float height;

            width = m_IsHorizontal ? textTransform.sizeDelta.x + m_DopWeight : rectTransform.sizeDelta.x;
            height = m_IsVertical ? textTransform.sizeDelta.y + m_DopHeight : rectTransform.sizeDelta.y;

            rectTransform.sizeDelta = new Vector2(width, height);
        }
        #endregion
    }
}
