using UnityEngine;
using UnityEngine.UI;

public class Shop_Item : MonoBehaviour
{
    public string itemName;
    public int woodCost = 1;
    public int ironCost = 1;
    public GameObject itemPrefab; // 구매 시 지급될 아이템

    public void OnBuyButtonClick()
    {
        if (Inventory.instance.CanAfford(woodCost, ironCost))
        {
            Inventory.instance.ConsumeResources(woodCost, ironCost);
            
            // 아이템 지급 로직 (예: 플레이어 위치에 생성)
            // Instantiate(itemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
            
            Debug.Log($"{itemName} 구매 성공!");
        }
        else
        {
            Debug.Log("재료가 부족합니다!");
        }
    }
}