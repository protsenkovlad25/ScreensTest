using DG.Tweening;
using ScreenTest.General;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ScreenTest.UI
{
    public class Panel : MonoBehaviour, IIniting
    {
        protected enum Direction { Center, Up, Down, Left, Right }

        #region Serialize Fields
        [Header("Anim Values")]
        [SerializeField] protected Direction m_Direction;
        [SerializeField] protected float m_OpenTime;
        [SerializeField] protected float m_CloseTime;

        [Header("Lock")]
        [SerializeField] protected Image m_LockImage;
        #endregion

        #region Fields
        protected Vector2 m_OpenPos;
        protected Vector2 m_ClosePos;

        protected Sequence m_OpenSeq;
        protected Sequence m_CloseSeq;
        protected RectTransform m_RectTransform;
        #endregion

        #region Methods
        public virtual void Init()
        {
            m_RectTransform = GetComponent<RectTransform>();
            
            InitStartPosition();
        }

        protected virtual void InitStartPosition()
        {
            if (m_Direction != Direction.Center)
            {
                m_OpenPos = m_RectTransform.anchoredPosition;

                float posX = m_Direction switch
                {
                    Direction.Up => m_RectTransform.anchoredPosition.x,
                    Direction.Down => m_RectTransform.anchoredPosition.x,
                    Direction.Left => -m_RectTransform.sizeDelta.x,
                    Direction.Right => m_RectTransform.sizeDelta.x
                };
                float posY = m_Direction switch
                {
                    Direction.Up => m_RectTransform.sizeDelta.y,
                    Direction.Down => -m_RectTransform.sizeDelta.y,
                    Direction.Left => m_RectTransform.anchoredPosition.y,
                    Direction.Right => m_RectTransform.anchoredPosition.y
                };

                m_ClosePos = new Vector2(posX, posY);
                m_RectTransform.anchoredPosition = m_ClosePos;
            }
        }

        #region OpenClose
        public virtual void Open(UnityAction onEndAction = null)
        {
            OpenAnim(onEndAction);
        }
        public virtual void Close(UnityAction onEndAction = null)
        {
            CloseAnim(onEndAction);
        }
        #endregion

        #region Animations
        protected virtual void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            onEndAction?.Invoke();
        }
        protected virtual void CloseAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(false);

            onEndAction?.Invoke();
        }
        #endregion

        #region Lock
        public void LockPanel()
        {
            m_LockImage.gameObject.SetActive(true);
        }
        public void UnlockPanel()
        {
            m_LockImage.gameObject?.SetActive(false);
        }
        #endregion

        #endregion
    }
}
