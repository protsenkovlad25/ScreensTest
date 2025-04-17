using DG.Tweening;
using ScreenTest.General;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ScreenTest.UI
{
    public class InfoPanel : Panel
    {
        #region Serialize Fields
        [SerializeField] private float m_StayTime;

        [Header("Text")]
        [SerializeField] private TMP_Text m_Text;
        #endregion

        #region Fields
        private Timer m_StayTimer;
        #endregion

        #region Methods
        public override void Init()
        {
            base.Init();

            InitTimer();
        }

        public void SetText(string text)
        {
            m_Text.text = text;
        }

        #region Timer
        private void InitTimer()
        {
            m_StayTimer = new Timer(m_StayTime);
            m_StayTimer.OnTimesUp.AddListener(EndTimer);
        }
        private void StartTimer()
        {
            m_StayTimer.Reset();
        }
        private void EndTimer()
        {
            Close();
        }
        #endregion

        #region Animations
        protected override void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            StartTimer();

            m_CloseSeq?.Kill();

            Sequence openSeq = DOTween.Sequence();
            m_OpenSeq = openSeq;

            openSeq.Append(m_RectTransform.DOAnchorPos(m_OpenPos, m_OpenTime));

            if (onEndAction != null)
                openSeq.AppendCallback(onEndAction.Invoke);

            openSeq.SetUpdate(true);
        }
        protected override void CloseAnim(UnityAction onEndAction = null)
        {
            m_OpenSeq?.Kill();

            Sequence closeSeq = DOTween.Sequence();
            m_CloseSeq = closeSeq;

            closeSeq.Append(m_RectTransform.DOAnchorPos(m_ClosePos, m_CloseTime));
            closeSeq.AppendCallback(() => { gameObject.SetActive(false); });

            if (onEndAction != null)
                closeSeq.AppendCallback(onEndAction.Invoke);

            closeSeq.SetUpdate(true);
        }
        #endregion

        private void Update()
        {
            if (!m_StayTimer.IsPause)
                m_StayTimer.Update();
        }
        #endregion
    }
}
