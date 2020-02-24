using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;

    [SerializeField]
    protected Transform pointA,pointB ;


    private Vector3 _currentTarget;
    private Animator _anim;
    private SpriteRenderer _sprite;

    public virtual void Init()
    {
		_anim   = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

	// Start is called before the first frame update
    private void Start()
    {
    	Init();
    }
	// Update is called once per frame
    public virtual void Update()
    {
  		if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
        	return;
        }

        Mouvement();
    }


    public virtual void Mouvement()
    {
       if (_currentTarget == pointA.position)
        {
        	_sprite.flipX = true  ;
     	} else
     	{
     		_sprite.flipX = false ;
     	}
     	if (transform.position == pointA.position)
        {
        	_currentTarget = pointB.position ;
        	_anim.SetTrigger("Idle");
        	_sprite.flipX = false ;
     	} else if (transform.position == pointB.position)
     	{
     		_currentTarget = pointA.position ;
     		_anim.SetTrigger("Idle");
     		_sprite.flipX = true ;
     	}

     	transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed *  Time.deltaTime ) ; 
    }


}
