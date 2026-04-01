using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotData
{
    public ItemData item;
    public int count;

    public SlotData(ItemData newItem, int newCount)
    {
        item = newItem;
        count = newCount;
    }
}

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<SlotData> slots = new List<SlotData>();
    public int maxSpace = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public bool Add(ItemData newItem, int amount = 1)
    {
        if (newItem.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == newItem)
                {
                    slot.count += amount;
                    onItemChangedCallback?.Invoke();
                    return true;
                }
            }
        }

        if (slots.Count >= maxSpace)
        {
            Debug.Log("가방이 가득 찼습니다!");
            return false;
        }

        slots.Add(new SlotData(newItem, amount));
        onItemChangedCallback?.Invoke();
        return true;
    }

    public bool CanAfford(ItemData targetItem, int amount)
    {
        int total = 0;
        foreach (var slot in slots)
        {
            if (slot.item == targetItem) total += slot.count;
        }
        return total >= amount;
    }

    public void Consume(ItemData targetItem, int amount)
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (slots[i].item == targetItem)
            {
                if (slots[i].count > amount)
                {
                    slots[i].count -= amount;
                    amount = 0;
                }
                else
                {
                    amount -= slots[i].count;
                    slots.RemoveAt(i);
                }
            }
            if (amount <= 0) break;
        }
        onItemChangedCallback?.Invoke();
    }

    // --- 여기부터 새로 추가할 함수들입니다 ---

    // 1. 특정 아이템이 있는지 확인하고 로그 찍기
    public void PrintInventoryContents()
    {
        if (slots.Count == 0)
        {
            Debug.Log("<color=yellow><b>[Inventory]</b> 가방이 텅 비어 있습니다.</color>");
            return;
        }

        string report = "<b>[현재 인벤토리 목록]</b>\n";
        for (int i = 0; i < slots.Count; i++)
        {
            report += $"Slot {i}: {slots[i].item.itemName} ({slots[i].count}개)\n";
        }
        report += $"<color=cyan>사용 중인 칸: {slots.Count} / {maxSpace}</color>";

        Debug.Log(report);
    }
}