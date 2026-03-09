using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float runSpeed;

    Rigidbody2D rigid;
    Animator anim;
    float currentSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();//Rigidbody2D 컴포넌트 가져오기
        anim = GetComponent<Animator>(); //Animator 컴포넌트 가져오기
    }

    void Update()
    {
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
