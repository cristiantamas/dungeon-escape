using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{

    public AcidEffect acidEffect;

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }


    public void Damage()
    {
        Health--;

        if(Health < 1){
            anim.SetDeath();
            transform.GetComponent<BoxCollider2D>().enabled = false;

            CreateGem(gems);
        }
    }

    public override void Update(){

    }

    internal void Attack()
    {
        Instantiate(acidEffect, transform.position, Quaternion.identity);
    }
}
