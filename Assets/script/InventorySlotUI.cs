using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI countText;

    public void UpdateSlotUI(SlotData slot)
    {
        if (slot != null && slot.item != null)
        {
            icon.sprite = slot.item.icon;
            icon.enabled = true;

            // 중첩 가능한 아이템이고 개수가 1보다 클 때만 숫자 표시
            if (slot.item.isStackable && slot.count > 1)
                countText.text = slot.count.ToString();
            else
                countText.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }
}