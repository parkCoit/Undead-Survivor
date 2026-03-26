using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    public Scanner scanner;
    public Hand[] hands;
    public RuntimeAnimatorController[] aniCon;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable() {
        anim.runtimeAnimatorController = aniCon[GameManager.instance.playerId];
    }

    


    private void FixedUpdate() {
        if (!GameManager.instance.isLive)
        return;

        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);

    } 

    private void OnMove(InputValue value)
    {
        if (!GameManager.instance.isLive)
        return;
        
        inputVec = value.Get<Vector2>();
    }

    public void LateUpdate() {
        if (!GameManager.instance.isLive)
        return;
        
        if (inputVec.x != 0)
        {
            anim.SetFloat("Speed", inputVec.magnitude);
            spriter.flipX = inputVec.x < 0;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision) { // 충돌이 유지되는 동안 매 프레임 호출
        if (!GameManager.instance.isLive)
            return;
        
        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0)
        {
            for (int index=2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
