using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    void Awake()
    {
        // 이미 다른 플레이어 매니저가 존재한다면 (복제본이라면)
        if (Instance != null && Instance != this)
        {
            Debug.Log("중복된 플레이어 오브젝트를 파괴합니다.");
            Destroy(gameObject);
            return; // 아래 코드를 실행하지 않고 종료
        }

        // 내가 유일한 인스턴스라면
        Instance = this;
        
        // 부모 오브젝트가 있다면 DontDestroyOnLoad가 작동하지 않으므로 부모 해제
        transform.SetParent(null);
        
        // 씬이 넘어가도 파괴되지 않게 설정
        DontDestroyOnLoad(gameObject);
    }
}