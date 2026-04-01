using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // GameObject 대신 스크립트 타입으로 선언하면, 
    // .gameObject로 끄고 켜는 것과 함수 호출을 동시에 할 수 있어요.
    public InventoryUI inventoryUI;
    private bool isInventoryOpen = false;

    void Start()
    {
        // 처음엔 비활성화
        inventoryUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        // 스크립트가 붙은 '오브젝트'를 끄고 켭니다.
        inventoryUI.gameObject.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            // 이제 빨간불 났던 'Instance' 없이, 
            // 위에서 연결한 inventoryUI 변수로 바로 실행합니다!
            inventoryUI.UpdateUI();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}