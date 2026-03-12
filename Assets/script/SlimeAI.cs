using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    [Header("슬라임 설정")]
    public float maxHealth = 30f;      // 최대 체력
    public float moveSpeed = 2f;       // 이동 속도
    public float chaseRange = 5f;      // 추적 범위
    public float attackRange = 1.2f;   // 공격 범위
    public float dashSpeed = 5f;
    public float attackCooldown = 2f;

    private float _currentHealth;
    private bool _isDead = false;       // 죽었는지 확인하는 변수
    private float _attackTimer = 0f;
    
    private Transform _player;
    private Animator _anim;
    

    void Start()
    {
        _currentHealth = maxHealth;
        _anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
        {
            _player = playerObj.transform;
        }
    }

    void Update()
    {
        // 죽었거나 플레이어가 없으면 아무 행동도 하지 않음
        if (_isDead || !_player) return;

        // 현재 재생 중인 애니메이션이 피격(Hit)이라면 이동이나 공격을 멈춤 (경직 효과)
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Hit")) return;

        if (_attackTimer > 0)
        {
            _attackTimer -= Time.deltaTime;
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Attack"))
        {
            float currentDist = Vector2.Distance(transform.position, _player.position);
            if (currentDist > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, dashSpeed * Time.deltaTime);
            }
            
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (_attackTimer <= 0f)
            {
                Attack();
                _attackTimer = attackCooldown;
            }
            else
            {
                Idle();
            }
        }
        else if (distanceToPlayer <= chaseRange)
        {
            Chase();
        }
        else
        {
            Idle();
        }
    }

    // 외부(플레이어의 공격 스크립트 등)에서 이 함수를 불러와 데미지를 줍니다.
    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        
        // 체력이 0 이하가 되면 사망, 아니면 피격 애니메이션 실행
        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // 방아쇠(Trigger)를 당겨 피격 애니메이션 실행
            _anim.SetTrigger("doHit");
        }
    }

    void Die()
    {
        _isDead = true;
        _anim.SetTrigger("doDie"); // 사망 애니메이션 실행
        
        // 이동/공격 멈춤
        _anim.SetBool("isMoving", false);
        _anim.SetBool("isAttacking", false);

        // 죽은 후 1.5초 뒤에 오브젝트를 게임에서 완전히 삭제
        Destroy(gameObject, 1.5f); 
    }

    void Chase()
    {
        _anim.SetBool("isMoving", true);
        _anim.SetBool("isAttacking", false);
        transform.position = Vector2.MoveTowards(transform.position, _player.position, moveSpeed * Time.deltaTime);

        if (_player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    void Attack()
    {
        _anim.SetBool("isMoving", false);
        _anim.SetTrigger("doAttack");
    }

    void Idle()
    {
        _anim.SetBool("isMoving", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}