using UnityEngine;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float runSpeed;
    bool isSwordEquipped = false;//칼 장착상태 여부 체크

    Rigidbody2D rigid;
    Animator anim;
    float currentSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();//Rigidbody2D 컴포넌트 가져오기
        anim = GetComponent<Animator>(); //Animator 컴포넌트 가져오기

        //플레이어 추적 카메라 가져오기
        CinemachineCamera vcam = FindFirstObjectByType<CinemachineCamera>();

        if (vcam != null)
        {
            vcam.Follow = transform;
        }
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.R)) //임시로 R키 누르면 칼 획득
        {
            isSwordEquipped = !isSwordEquipped; // 상태 반전
            anim.SetBool("isSword", isSwordEquipped); // 애니메이터 파라미터 업데이트
        }

        if (Input.GetKeyDown(KeyCode.A) && isSwordEquipped) //공격
        {
            anim.SetTrigger("doAttack");
        }


        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        //shift 키를 누르고있고 이동중일때만 달리기속도 적용
        bool isRun = Input.GetKey(KeyCode.LeftShift) && inputVec.magnitude != 0;
        currentSpeed = isRun ? runSpeed : speed;

        float animSpeed = inputVec.magnitude;
        if (isRun) animSpeed *= 2f;//달리기 상태일때 애니메이터의 Speed파라미터를 넘겨줌
        
        anim.SetFloat("Speed", animSpeed);

        if (inputVec.magnitude != 0){
            //입력값에 따른 애니메이터 파라미터 업데이트
            anim.SetFloat("InputX", inputVec.x);
            anim.SetFloat("InputY", inputVec.y);
        }
    }

    void FixedUpdate()
    {   
        //키보드 입력값 계속 받아서 위치 업데이트
        Vector2 nextVec = inputVec.normalized * currentSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
}
