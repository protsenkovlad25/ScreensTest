using ScreenTest.General.Configs;
using ScreenTest.General.Sound;
using ScreenTest.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ScreenTest.General
{
    public class ShopController : MonoBehaviour, IController
    {
        #region Serialize Fields
        [Header("Controllers")]
        [SerializeField] private InterfaceController m_InterfaceController;
        
        [Header("Panel")]
        [SerializeField] private ShopPanel m_ShopPanel;
        [SerializeField] private PopupPanel m_PopupPanel;

        [Header("Prefabs")]
        [SerializeField] private ProductSlot m_ProductSlotPrefab;
        [SerializeField] private PopupPanel m_PopupPanelPrefab;
        #endregion

        #region Fields
        private List<ProductSlot> m_ProductSlots;
        private ProductSlot m_SelectedSlot;
        #endregion

        #region Methods
        public void Init()
        {
            InitSlots(Resources.Load<ProductsConfig>("Configs/ProductsConfig").ProductDatas);
        }

        public void InitSlots(List<ProductData> datas)
        {
            m_ProductSlots = new List<ProductSlot>();

            ProductSlot slot;
            for (int i = 0; i < datas.Count; i++)
            {
                slot = InstantiateSlot(datas[i]);
                slot.Init();

                m_ProductSlots.Add(slot);
            }
        }

        private ProductSlot InstantiateSlot(ProductData data)
        {
            ProductSlot slot = Instantiate(m_ProductSlotPrefab, m_ShopPanel.ProductSlotsParent);

            slot.name = $"Product_{m_ProductSlots.Count}";
            slot.OnSlotClick = ClickedSlot;
            slot.SetData(data);

            return slot;
        }

        private void ClickedSlot(ProductSlot slot)
        {
            m_SelectedSlot = slot;

            m_ShopPanel.LockPanel();
            OpenPopup();
        }

        private void OpenPopup()
        {
            m_PopupPanel.OnYesClick = BuyProduct;
            m_PopupPanel.OnNoClick = CancelBuy;

            m_PopupPanel.SetText($"{m_SelectedSlot.Data.Name}\nПокупаете? :>", "Купить", "Отмена");

            m_InterfaceController.OpenPanel(m_PopupPanel);
        }
        private void ClosePopup()
        {
            m_InterfaceController.ClosePanel(m_PopupPanel);
        }

        private void BuyProduct()
        {
            Debug.Log($"Buy Product - {m_SelectedSlot.Data.Name} ({m_SelectedSlot.name})");

            SoundManager.Instance.PlayEffect(SoundType.UI, "Buying", Vector3.zero);

            m_InterfaceController.OpenInfoPanel($"Куплен продукт: {m_SelectedSlot.Data.Name}");

            m_SelectedSlot = null;

            m_ShopPanel.UnlockPanel();
            ClosePopup();
        }
        private void CancelBuy()
        {
            m_SelectedSlot = null;

            m_ShopPanel.UnlockPanel();
            ClosePopup();
        }
        #endregion
    }
}
