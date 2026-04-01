using UnityEngine;

[System.Serializable] // 이 줄이 있어야 유니티 인스펙터에 리스트 항목들이 보입니다.
public class ResourceCost
{
    public ItemData item; // 재료 아이템 에셋
    public int amount;    // 필요한 개수
}