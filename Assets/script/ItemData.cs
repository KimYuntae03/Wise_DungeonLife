using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;       // 아이템 이름
    public Sprite icon;           // 아이콘 이미지
    public bool isStackable;      // 겹칠 수 있는가? (재료는 체크, 장비는 미체크)
    
    [TextArea]
    public string description;    // 아이템 설명
}