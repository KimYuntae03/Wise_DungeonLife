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
            if (!obj.CompareTag("Monster")) continue;

            //피격범위 안에 있는지 확인
            Vector2 dirToEnemy = (obj.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(lookDir, dirToEnemy);

            if (angle <= attackAngle * 0.5f)
            {
                Debug.Log($"{obj.name} 명중!");
                //앞으로 여기에 피격시 데미지 적용 코드 추가할거임
            }
        }
    }
}
