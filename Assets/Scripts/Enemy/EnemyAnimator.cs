using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _anim;
    void Start(){
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetIdle(){
        _anim.SetTrigger("Idle");
    }

    public void SetHit(){
        _anim.SetTrigger("Hit");
    }

    public void SetDeath(){
        _anim.SetTrigger("Death");
    }

    public void SetInCombat(bool value){
        _anim.SetBool("InCombat", value);
    }

    public bool CheckAnimationName(string name){
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }
}
