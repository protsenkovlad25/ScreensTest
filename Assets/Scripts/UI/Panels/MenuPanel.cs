using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScreenTest.UI
{
    public class MenuPanel : Panel
    {
        #region Serialize Fields
        [Header("Objects")]
        [SerializeField] private Image m_LogoImage;
        [SerializeField] private Transform m_ButtonsContainer;
        #endregion

        #region Fields
        private Vector2 m_TextStartPos;
        private Vector2 m_TextClosePos;

        private RectTransform m_TextRT;
        #endregion

        #region Methods
        protected override void InitStartPosition()
        {
            base.InitStartPosition();

            m_TextRT = m_LogoImage.GetComponent<RectTransform>();
            m_TextStartPos = m_TextRT.anchoredPosition;
            m_TextClosePos = new Vector2(m_TextRT.anchoredPosition.x, m_TextRT.sizeDelta.y);

            m_TextRT.anchoredPosition = m_TextClosePos;

            m_ButtonsContainer.DOScale(0, 0);
        }

        #region Animations
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_TextRT.DOAnchorPos(m_TextStartPos, m_OpenTime));
            openSeq.Join(m_ButtonsContainer.DOScale(1f, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_TextRT.DOAnchorPos(m_TextClosePos, m_CloseTime));
            closeSeq.Join(m_ButtonsContainer.DOScale(0f, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion

        #endregion
    }
}
