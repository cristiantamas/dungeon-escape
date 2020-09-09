using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected EnemyAnimator anim;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    private Vector3 _target, direction;

    protected Player player;
    public Diamond diamond;

    private void Start()
    {
        Init();
    }

    public virtual void Init(){
        anim = GetComponent<EnemyAnimator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    public virtual void Update(){
        if (anim.CheckAnimationName("Death")) return;

        if (IsCombatStance()) return;

        if (anim.CheckAnimationName("Idle")) return;

        if (anim.CheckAnimationName("Hit")) return;

        Movement();
    }

    public virtual void Movement(){
        if (_target == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            _target = pointB.position;
            anim.SetIdle();
        }
        else if (transform.position == pointB.position)
        {
            _target = pointA.position;
            anim.SetIdle();
        }

        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
    }

    private bool IsCombatStance(){
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distance <= 2.0f)
        {
            anim.SetIdle();
            anim.SetInCombat(true);

            direction = player.transform.localPosition - transform.localPosition;
            if (direction.x < 0)
                spriteRenderer.flipX = true;
            else if (direction.x > 0)
                spriteRenderer.flipX = false;


            return true;
        }

        anim.SetInCombat(false);
        return false;
    }

    public void CreateGem(int value){
        Instantiate(diamond, transform.position, Quaternion.identity);
        diamond.value = value;
    }
}
