using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // 어디서든 접근 가능하게 싱글톤 설정
    
    public int wood = 10; // 테스트용 초기값
    public int iron = 10;

    void Awake() { instance = this; }

    // 재료가 충분한지 확인하는 함수
    public bool CanAfford(int woodCost, int ironCost)
    {
        return wood >= woodCost && iron >= ironCost;
    }

    // 재료 소비 함수
    public void ConsumeResources(int woodCost, int ironCost)
    {
        wood -= woodCost;
        iron -= ironCost;
        Debug.Log($"남은 재료 - 나무: {wood}, 철: {iron}");
    }
}