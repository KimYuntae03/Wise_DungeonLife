using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

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

    void OnEnable()
    {
        //씬이 바뀔 때마다 OnSceneLoaded함수 실행
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        //메모리 누수 방지로 오브젝트 비활성화 될 때 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {   
        //현재 씬의 CinemachineCamera찾기
        var vcam = FindFirstObjectByType<CinemachineCamera>();

        if (vcam != null)
        {
            //현재 이 스크립트가 붙어있는 Player를 따라가도록 설정
            vcam.Follow = transform;
        }
    }
}