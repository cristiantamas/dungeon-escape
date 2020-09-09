using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;
    private Animator _swordAnimator;

    // Start is called before the first frame update
    void Start(){
        _anim = GetComponentInChildren<Animator>();
        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move){
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jump){
        _anim.SetBool("Jumping", jump);
    }

    public void Attack(){
        _anim.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAnimation");
    }

    public void Die(){
        _anim.SetTrigger("Death");
    }

    public bool CheckAnimationName(string name) {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
