using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{   
    
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public void Damage(){

        Health--;
        anim.SetHit();
        anim.SetInCombat(true);

        if(Health == 0){
            anim.SetDeath();
            transform.GetComponent<BoxCollider2D>().enabled = false;

            CreateGem(gems);
        }

    }
}
