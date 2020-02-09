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

	private bool  _resetJump = false ;
    // Start is called before the first frame update
    void Start()
    {
        // assign the handle to regid body
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    	Mouvement();

    }

    void Mouvement ()
    {
        //Horizental Input for left and righ
    	float move = Input.GetAxisRaw("Horizontal");
    	//current velocity = new velocity (x, current.velocity.y);
        _rigid.velocity = new Vector2(move * _speed,_rigid.velocity.y);
		if( Input.GetKeyDown(KeyCode.Space) && isGrounded()==true)
		{
			_rigid.velocity = new Vector2(_rigid.velocity.x,_jumpforce);
			 StartCoroutine(ResetJumRoutine());
		}

    }
    bool isGrounded()
    {
    	RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down , 0.6f, 1<<8) ;
    	if (hitInfo.collider != null )
    	{
    		if(_resetJump==false)
    		{
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

}
