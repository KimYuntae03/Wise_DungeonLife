using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    Animator anim;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();//Rigidbody2D 컴포넌트 가져오기
        anim = GetComponent<Animator>(); //Animator 컴포넌트 가져오기
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Speed", inputVec.sqrMagnitude);
        
        if (inputVec.magnitude != 0){
            //입력값에 따른 애니메이터 파라미터 업데이트
            anim.SetFloat("InputX", inputVec.x);
            anim.SetFloat("InputY", inputVec.y);
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
}
