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
	private bool _grounded = false ;
	[SerializeField]
	private LayerMask _groundLayer ;

	private bool  resetJumpNeeded = false ;
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

    	CheckGrounded() ;
    }

    void Mouvement ()
    {
        //Horizental Input for left and righ
    	float move = Input.GetAxisRaw("Horizontal");
    	// if space key && grounded == true
    	if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
    	{

    		_rigid.velocity = new Vector2(_rigid.velocity.x,_jumpforce);
    		_grounded = false ;
    		 resetJumpNeeded = true; 
    		  StartCoroutine(ResetJumpNeededRoutine());
    	}
    	//current velocity = new velocity (x, current.velocity.y);
        _rigid.velocity = new Vector2(move,_rigid.velocity.y);
    }

    void CheckGrounded()
    {
   		// Cast a ray straight down.
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down * 0.6f , 1.0f , /*1 << 8*/ _groundLayer.value);
        Debug.DrawRay(transform.position,Vector2.down * 0.6f , Color.green);
        if (hitInfo.collider != null )
        {
        	Debug.Log("Hit collider name[" + hitInfo.collider.name +"]");

      		if (resetJumpNeeded == false )
        	{
        		_grounded = true ;
        	}
        }

    }
    IEnumerator ResetJumpNeededRoutine()
    {
    	yield return new WaitForSeconds(0.1f); 
    	resetJumpNeeded = false ;
    }
}
