using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ScreenTest.UI
{
    public class SettingsPanel : Panel
    {
        #region Actions
        public System.Action<string, bool> OnSettingChanged;
        #endregion

        #region Serialize Fields
        [Header("Objects")]
        [SerializeField] private TMP_Text m_TopText;
        [SerializeField] private Button m_BackButton;
        [SerializeField] private Transform m_TogglesContainer;

        [Header("Settings")]
        [SerializeField] private List<ToggleSettingSlot> m_ToggleSettings;
        #endregion
        
        #region Fields
        private Vector2 m_TextStartPos;
        private Vector2 m_TextClosePos;
        private Vector2 m_ButtonStartPos;
        private Vector2 m_ButtonClosePos;

        private RectTransform m_TextRT;
        private RectTransform m_ButtonRT;
        #endregion

        #region Methods
        public override void Init()
        {
            base.Init();

            foreach (var setting in m_ToggleSettings)
            {
                setting.OnToggleChanged = OnSettingChanged.Invoke;
                setting.Init();
            }
        }
        protected override void InitStartPosition()
        {
            base.InitStartPosition();

            m_ButtonRT = m_BackButton.GetComponent<RectTransform>();
            m_ButtonStartPos = m_ButtonRT.anchoredPosition;
            m_ButtonClosePos = new Vector2(-m_ButtonRT.sizeDelta.x, m_ButtonRT.anchoredPosition.y);
            m_ButtonRT.anchoredPosition = m_ButtonClosePos;

            m_TextRT = m_TopText.GetComponent<RectTransform>();
            m_TextStartPos = m_TextRT.anchoredPosition;
            m_TextClosePos = new Vector2(m_TextRT.anchoredPosition.x, m_TextRT.sizeDelta.y);
            m_TextRT.anchoredPosition = m_TextClosePos;

            m_TogglesContainer.DOScale(0, 0);
        }

        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_ButtonRT.DOAnchorPos(m_ButtonStartPos, m_OpenTime));
            openSeq.Join(m_TextRT.DOAnchorPos(m_TextStartPos, m_OpenTime));
            openSeq.Join(m_TogglesContainer.DOScale(1f, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_ButtonRT.DOAnchorPos(m_ButtonClosePos, m_CloseTime));
            closeSeq.Join(m_TextRT.DOAnchorPos(m_TextClosePos, m_CloseTime));
            closeSeq.Join(m_TogglesContainer.DOScale(0f, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
    }
}
