using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	// Get reference to regid body
	private Rigidbody2D _rigid;
	// variable for jump force
	[SerializeField]
	private float _jumpforce = 5.0f;

	[SerializeField]
	private float _speed = 5.0f;

	private bool  _grounded = false ;
	private bool  _resetJump = false ;

	private PlayerAnimation _palyerAnim ;

	private SpriteRenderer _playerSprite ;
	private SpriteRenderer _swordArcSprite ;

    // Start is called before the first frame update
    void Start()
    {
        // assign the handle to regid body
        _rigid = GetComponent<Rigidbody2D>();
        _palyerAnim  = GetComponent<PlayerAnimation>();
        _playerSprite  = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    	Mouvement();

    	if ( Input.GetMouseButtonDown(0) && isGrounded()==true )
    	{
    		 _palyerAnim.Attack();
    	}

    }

    void Mouvement ()
    {
        //Horizental Input for left and righ
    	float move = Input.GetAxisRaw("Horizontal");

		_grounded = isGrounded();

    	if (move > 0)
    	{
    		PlayerFlipX(true);		
    	}
    	else if (move < 0)
    	{
    		PlayerFlipX(false);	
    	}
        
		if( Input.GetKeyDown(KeyCode.Space) && isGrounded()==true)
		{
			_rigid.velocity = new Vector2(_rigid.velocity.x,_jumpforce);
			 StartCoroutine(ResetJumRoutine());
			 _palyerAnim.Jump(true);
		}

		_rigid.velocity = new Vector2(move * _speed,_rigid.velocity.y);
		_palyerAnim.Move(move);

    }
    bool isGrounded()
    {
    	RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down , 0.6f, 1<<8) ;
    	Debug.DrawRay(transform.position, Vector2.down , Color.red);

    	if (hitInfo.collider != null )
    	{
    		if(_resetJump==false)
    		{
    			_palyerAnim.Jump(false);
    			return true;
    		}
    	}

    	return false;
    }
    IEnumerator ResetJumRoutine()
    {
    	_resetJump = true ;
    	yield return new WaitForSeconds(0.1f);
		_resetJump = false ;
    }

    void PlayerFlipX (bool facingRight)
    {
    	if (facingRight == true)
    	{
    		_playerSprite.flipX = false ;	
    		_swordArcSprite.flipX= false ;
    		_swordArcSprite.flipY= false ;

    		Vector3 newPos = _swordArcSprite.transform.localPosition ;
    		newPos.x = 1.01f;
    		_swordArcSprite.transform.localPosition = newPos ;

    	}
    	else  if (facingRight == false)
    	{
    		_playerSprite.flipX = true ;
    		_swordArcSprite.flipX= true ;
    		_swordArcSprite.flipY= true ;

    		Vector3 newPos = _swordArcSprite.transform.localPosition ;
    		newPos.x = -1.01f;
    		_swordArcSprite.transform.localPosition = newPos ;

    	}
        
    }

}
