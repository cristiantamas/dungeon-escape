using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable 
{
    
    public int Health { get; set; }
    public int gems;

    private PlayerAnimation _anim;

    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcRenderer;

    [SerializeField] private float _jumpForce = 5.5f;
    [SerializeField] private float _speed = 2.5f;

    private bool _resetJump = false;
    private bool _grounded = false;

    private float _angle1 = 225.0f;
    private float _angle2 = 315.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;
    }

    // Update is called once per frame
    void Update(){
        
        _grounded = IsGrounded();

        if (_anim.CheckAnimationName("Death")) return;

        Movement();
        CheckAttack();
    }

    void CheckAttack(){
        _grounded = IsGrounded();

        if (_grounded && CrossPlatformInputManager.GetButtonDown("A_Button")) {
            _anim.Attack();
        }
    }

    void Movement(){
        float move = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");

        Flip(move);

        if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.Space)) && _grounded){
            Debug.Log("Intra");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());

            _anim.Jump(true);
        }

        if(_rigid != null)
            _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _anim.Move(move);
    }

    bool IsGrounded(){
        /* Use 45 degrees directirons to the left and right to fix jumping at edges */
        Vector2 direction1 = GetDirectionVector2D(_angle1);
        Vector2 direction2 = GetDirectionVector2D(_angle2);

        RaycastHit2D hitInfo1 = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 8);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position, direction1, 1.2f, 1 << 8);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position, direction2, 1.2f, 1 << 8);

        Debug.DrawRay(transform.position, direction1 * 0.8f, Color.red);
        Debug.DrawRay(transform.position, direction2 * 0.8f, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.green);
        if (hitInfo1.collider != null || hitInfo2.collider != null || hitInfo3.collider != null)
        {
            if (_resetJump == false)
            {
                _anim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void Flip(float move){
        /* Flip character sprite if needed*/
        if (move > 0)
        {
            _spriteRenderer.flipX = false;
            _swordArcRenderer.flipX = false;
            _swordArcRenderer.flipY = false;

            /* Flip the position of the sword animation */
            Vector3 position = _swordArcRenderer.transform.localPosition;
            position.x = 1.01f;

            _swordArcRenderer.transform.localPosition = position;
        }
        else if (move < 0)
        {
            _spriteRenderer.flipX = true;
            _swordArcRenderer.flipX = true;
            _swordArcRenderer.flipY = true;

            /* Flip the position of the sword animation */
            Vector3 position = _swordArcRenderer.transform.localPosition;
            position.x = -1.01f;

            _swordArcRenderer.transform.localPosition = position;
        }

    }

    IEnumerator ResetJumpRoutine(){
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }


    /* Used for casting a ray at a 45 degrees angle - removes jumping bugs*/
    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }


    public void Damage(){

        Health--;

        if (Health < 1) {
            _anim.Die();
            transform.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(transform.GetComponent<Rigidbody2D>());
        }
        
        UIManager.Instance.UpdateLives(Health);
    }

    public void AddGems(int amount){
        gems += amount;
        UIManager.Instance.UpdateGemCount(gems);
    }
}
