using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Animator animator;

    // 상태 변수
    private bool isJumping = false;

    float horizontalMove;
    float verticalMove;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
            GetComponent<Animator>().Play("Jump");


        AnimationUpdate();
    }

    // 애니메이션 업데이트
    private void AnimationUpdate() {
        if (horizontalMove == 0 && verticalMove == 0)
        {
            GetComponent<Animator>().Play("Idle");
        }
        else
        {
            GetComponent<Animator>().Play("Run");
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        }
    }
}
