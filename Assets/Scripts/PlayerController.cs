using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int jump = 1250;
    public float velocity = 5f;
    Vector3 localScale;

    Rigidbody2D rigidbody;
    Animator animator;
    public SpriteRenderer helmet;

    // 시작(or 부활)하자마자 블록과 캐릭터가 중첩되는 경우 대비
    bool ready = false;
    public bool Ready
    {
        get { return ready; }
        set { ready = value; }
    }

    ObjColor color;
    public ObjColor Color
    {
        get { return color; }
        set
        {
            color = value;

            switch (value)
            {
                case ObjColor.Yellow:
                    helmet.material.SetFloat("_ColorR", 1);
                    helmet.material.SetFloat("_ColorG", 1);
                    helmet.material.SetFloat("_ColorB", 0);                   
                    break;

                case ObjColor.Red:
                    helmet.material.SetFloat("_ColorR", 1);
                    helmet.material.SetFloat("_ColorG", 0);
                    helmet.material.SetFloat("_ColorB", 0);
                    break;

                case ObjColor.Blue:
                    helmet.material.SetFloat("_ColorR", 0);
                    helmet.material.SetFloat("_ColorG", 0);
                    helmet.material.SetFloat("_ColorB", 1);
                    break;
            }
        }
    }
    
    private void Awake()
    {
        localScale = transform.localScale;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = UtilHelper.Find<Animator>(transform, "model");
        helmet = UtilHelper.Find<SpriteRenderer>(transform, "model/body/Head/Hat-Helmet");        
        Color = ObjColor.Red;
    }

    public void KeyInput(Vector2 dir)
    {
        if (!GameMng.canInput)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) // 쩜프
            Jump(dir.y);

        if (Input.GetKey(KeyCode.A)) // 방향키
        {
            SetAniState(1);
            transform.localScale = new Vector3(-1, 1, 1);
            Move(new Vector2(-1, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetAniState(1);
            transform.localScale = new Vector3(1, 1, 1);
            Move(new Vector2(1, 0));
        }
        else
            SetAniState();

        /* 모바일(인게임 조이스틱) 코드
        if (!dir.Equals(Vector2.zero))
        {
            Ready = true;
            if (dir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (dir.x > 0) transform.localScale = new Vector3(1, 1, 1);

            SetAniState(1);
            Move(dir);
        }
        else
        {
            SetAniState(0);
            Move(Vector2.zero);
        }
        */
    }

    public void SetAniState(int state = 0)
    {
        animator.SetInteger("State", state);
    }

    public void Move(Vector2 dir)
    {
        rigidbody.velocity = new Vector2(dir.x * velocity, rigidbody.velocity.y);
    }

    public void Jump(float jumpVal)
    {
        if (rigidbody.velocity.y != 0) return;
        if (jumpVal >= 0)
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(Vector2.up * jump);
        }
    }

    public void ResetPlayer(Vector3 pos, Vector3 dir)
    {
        ready = false;

        transform.position = pos;
        transform.rotation = Quaternion.identity;
        rigidbody.velocity = Vector2.zero;

        Vector3 scale = new Vector3(Mathf.Abs(localScale.x), localScale.y, localScale.z);
        if (dir.x < 0)
            scale.x *= -1;
        transform.localScale = scale;
    }
}
