using UnityEngine;

public class ItemCheater : MonoBehaviour
{
    [Header("테스트용 아이템 에셋")]
    public ItemData woodItem; // 프로젝트 창에서 'Wood' 에셋 드래그
    public ItemData ironItem; // 프로젝트 창에서 'Iron' 에셋 드래그

    void Update()
    {
        // K 키를 누르면 나무 20개 추가
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (woodItem != null)
            {
                Inventory.instance.Add(woodItem, 20);
                Debug.Log("나무 20개 치트 사용!");
            }
        }

        // L 키를 누르면 철 20개 추가
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (ironItem != null)
            {
                Inventory.instance.Add(ironItem, 20);
                Debug.Log("철 20개 치트 사용!");
            }
        }
        if (Input.GetKeyDown(KeyCode.P)) // P키를 누르면 가방 상태 출력
        {
            Inventory.instance.PrintInventoryContents();
        }
    }
}
