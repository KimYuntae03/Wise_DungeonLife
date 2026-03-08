using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    [Header("FirstFloor")]
    public string nextSceneName;

    private bool isPlayerInRange = false;

    void Update() 
    {
      if (isPlayerInRange == true && Input.GetKeyDown(KeyCode.E))
      {
        SceneManager.LoadScene(nextSceneName);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        isPlayerInRange = true;
        Debug.Log("포탈 앞에 도착! E키를 눌러 이동하세요.");
      }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        isPlayerInRange = false;
        Debug.Log("포탈에서 멀어졌습니다.");
      }
    }
}
