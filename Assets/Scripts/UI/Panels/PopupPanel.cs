using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ScreenTest.UI
{
    public class PopupPanel : Panel
    {
        #region Actions
        public System.Action OnYesClick;
        public System.Action OnNoClick;
        #endregion

        #region Serialize Fields
        [Header("Objects")]
        [SerializeField] private TMP_Text m_Text;
        [SerializeField] private Button m_YesButton;
        [SerializeField] private Button m_NoButton;
        #endregion

        #region Methods
        protected override void InitStartPosition()
        {
            base.InitStartPosition();

            transform.DOScale(0, 0);
        }

        #region Sets
        public void SetText(string text, string yesText = "", string noText = "")
        {
            m_Text.text = text;

            SetYesText(yesText);
            SetNoText(noText);
        }
        public void SetYesText(string text)
        {
            if (text != "") m_YesButton.GetComponentInChildren<TMP_Text>().text = text;
        }
        public void SetNoText(string text)
        {
            if (text != "") m_NoButton.GetComponentInChildren<TMP_Text>().text = text;
        }
        #endregion

        #region Clicks
        public void ClickYes()
        {
            OnYesClick?.Invoke();
        }
        public void ClickNo()
        {
            OnNoClick?.Invoke();
        }
        #endregion

        #region Animations
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(transform.DOScale(1f, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(transform.DOScale(0f, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion
        #endregion
    }
}
