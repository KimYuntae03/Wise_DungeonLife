using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotsParent;
    private InventorySlotUI[] uiSlots;

    // 오브젝트가 활성화(SetActive(true))될 때마다 호출됨
    void OnEnable()
    {
        // 1. [중요] 중복 방지를 위해 기존 연결을 한 번 끊고 다시 연결
        if (Inventory.instance != null)
        {
            Inventory.instance.onItemChangedCallback -= UpdateUI;
            Inventory.instance.onItemChangedCallback += UpdateUI;
        }

        // 2. 창이 열리는 순간, 현재 가방 데이터를 즉시 반영
        UpdateUI();
    }

    // 오브젝트가 비활성화(SetActive(false))되거나 파괴될 때 호출됨
    void OnDisable()
    {
        if (Inventory.instance != null)
        {
            Inventory.instance.onItemChangedCallback -= UpdateUI;
        }
    }

    // Awake는 오브젝트가 꺼져있어도 '처음 한 번'은 실행될 가능성이 높음 (부모가 켜질 때)
    void Awake()
    {
        // 미리 슬롯들을 찾아둡니다.
        if (slotsParent != null)
        {
            uiSlots = slotsParent.GetComponentsInChildren<InventorySlotUI>(true); // (true)는 비활성화된 자식도 찾음
        }
    }

    public void UpdateUI()
    {
        // 인벤토리 인스턴스가 없으면 실행 불가
        if (Inventory.instance == null) return;

        // 슬롯 UI 배열이 비어있으면 새로 가져오기 (비활성화된 자식까지 포함해서 찾기)
        if (uiSlots == null || uiSlots.Length == 0)
        {
            uiSlots = slotsParent.GetComponentsInChildren<InventorySlotUI>(true);
        }

        // 디버그용: 슬롯을 몇 개나 찾았는지 콘솔에 찍어줍니다. (안 보이면 이 숫자를 확인하세요!)
        // Debug.Log($"UI 업데이트 중... 찾은 슬롯 개수: {uiSlots.Length}");

        for (int i = 0; i < uiSlots.Length; i++)
        {
            if (uiSlots[i] == null) continue;

            // 데이터가 있는 칸인지 확인
            if (i < Inventory.instance.slots.Count && Inventory.instance.slots[i].item != null)
            {
                uiSlots[i].UpdateSlotUI(Inventory.instance.slots[i]);
            }
            else
            {
                uiSlots[i].ClearSlot();
            }
        }
    }
}