using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    public Scanner scanner;
    public Hand[] hands;

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
}
