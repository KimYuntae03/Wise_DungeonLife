using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [Header("구매 결과물")]
    public ItemData itemToBuy; // 구매 시 플레이어에게 줄 아이템

    [Header("필요한 재료 목록")]
    // 이제 인스펙터에서 + 버튼을 눌러 재료를 무제한으로 추가할 수 있습니다.
    public List<ResourceCost> costs = new List<ResourceCost>();

    public void Buy()
    {
        // 1. 모든 재료가 충분한지 한 번에 검사
        if (!CanAffordAll())
        {
            Debug.Log("재료가 부족합니다!");
            return;
        }

        // 2. 가방에 빈 공간이 있는지 체크 (인벤토리의 maxSpace 활용)
        if (Inventory.instance.slots.Count >= Inventory.instance.maxSpace)
        {
            Debug.Log("가방이 꽉 차서 더 이상 구매할 수 없습니다!");
            return;
        }

        // 3. 재료 소비 (리스트를 돌며 하나씩 뺌)
        foreach (var cost in costs)
        {
            Inventory.instance.Consume(cost.item, cost.amount);
        }

        // 4. 아이템 지급
        Inventory.instance.Add(itemToBuy);
        Debug.Log($"{itemToBuy.itemName} 구매 성공!");
    }

    // 재료 전체를 체크하는 함수
    private bool CanAffordAll()
    {
        foreach (var cost in costs)
        {
            // 하나라도 인벤토리에 부족하면 바로 false 반환
            if (!Inventory.instance.CanAfford(cost.item, cost.amount))
                return false;
        }
        return true;
    }
}