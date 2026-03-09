using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameObject shopUI; // 에디터에서 상점 UI 패널을 연결
    private bool isPlayerNearby = false;

    void Update()
    {
        // 플레이어가 근처에 있고 F키를 누르면 상점 오픈
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleShop();
        }
    }

    void ToggleShop()
    {
        bool isActive = !shopUI.activeSelf;
        shopUI.SetActive(isActive);
        
        // 상점이 열리면 게임 일시정지 (선택 사항)
        // Time.timeScale = isActive ? 0f : 1f; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            shopUI.SetActive(false); // 멀어지면 상점 닫기
        }
    }
}