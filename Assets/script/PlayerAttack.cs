using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f; //공격 범위
    public float attackAngle = 100f; //공격 각도
    public int damage = 10; 
    public LayerMask enemyLayer; //공격범위 시각화

    Animator anim;
    PlayerController playerController;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && anim.GetBool("isSword")) //Z키 입력받으면 공격 함수 실행
        {
            Attack();
        }
    }

    void Attack()
    {
        // doAttack파라미터 애니메이션 컨트롤러에 전달
        anim.SetTrigger("doAttack");

        // 판정 로직
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // 플레이어의 현재 방향 
        Vector2 lookDir = playerController.inputVec.magnitude > 0 ? 
                          playerController.inputVec.normalized : playerController.lastInputVec;

        foreach (Collider2D obj in hitEnemies)
        {   
            //피격범위 안에 있는지 확인
            Vector2 dirToEnemy = (obj.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(lookDir, dirToEnemy);

            if (angle <= attackAngle * 0.5f)
            {
                Debug.Log($"{obj.name} 명중!");

                //SlimeAI를 가진 오브젝트를 가져옴
                SlimeAI slime = obj.GetComponent<SlimeAI>(); 
                if (slime != null)
                {
                    slime.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()//피격범위 시각화
    {
        Gizmos.color = Color.blue;
        // 전체 원 범위 표시
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // 바라보는 방향을 기준으로 부채꼴의 양 끝 선 그리기
        Vector2 lookDir = (Application.isPlaying && playerController != null) ? 
            (playerController.inputVec.magnitude > 0 ? playerController.inputVec.normalized : playerController.lastInputVec) : 
            Vector2.up;

        Vector3 leftBoundary = Quaternion.Euler(0, 0, -attackAngle * 0.5f) * lookDir;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, attackAngle * 0.5f) * lookDir;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * attackRange);
    }
}
