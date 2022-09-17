using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurMovements : MonoBehaviour
{
    [SerializeField] private float moveSpeed=5f;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private LayerMask LayerMaskWithColliders;
    [SerializeField] private float numOfMoves=2;
    private Transform _movePoint;

    public static System.Action GameOver;
    
    void Awake()
    {
        _movePoint = this.gameObject.transform;
        PlayerController.PlayerMadeTurn += InterceptionInitiate;

    }

    private void OnDestroy()
    {
        PlayerController.PlayerMadeTurn -= InterceptionInitiate;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, moveSpeed * Time.deltaTime);
      
    }
    /// <summary>
    /// initializing interception of minotaur, after player was moved, firstly trying to move horizontally, if not, try vertically 
    /// </summary>
    /// <param name="x">x coords of player</param>
    /// <param name="y">y coords of player</param>
    void InterceptionInitiate(float x, float y)
    {
        for (int i = 0; i < numOfMoves; i++)
        {
            CalculateNextMove( x, y);
            DetectEnd(x,y);
        }
       
    }
    /// <summary>
    /// calculating to which tile minotaur will move next 
    /// </summary>
    /// <param name="x">x coords of player</param>
    /// <param name="y">y coords of player</param>
    void CalculateNextMove(float x, float y)
    {
        
        if (Math.Abs(transform.position.x-x)>=0.2f)
        {
            if (transform.position.x > x)
            {
                if (MoveMinotaur(transform.position.x - tileSize, transform.position.y))
                {
                    return;
                }
               
            }else if (Math.Abs(transform.position.x - x) < 0.01){}
            else
            {
                if (MoveMinotaur(transform.position.x + tileSize, transform.position.y))
                {
                    return;
                }
                
            }
        }
        if (Math.Abs(transform.position.y-y)>=0.2f)
        {
            if (transform.position.y > y)
            {
                MoveMinotaur(transform.position.x, transform.position.y - tileSize);
                return;
            }
            else if (Math.Abs(transform.position.y - y) < 0.01)return;
            else
            {
                MoveMinotaur(transform.position.x, transform.position.y + tileSize);
                return;
            }
        }
    }

   
    /// <summary>
    ///checks if there any colliders, and after that moves navigation point of minotaur 
    /// </summary>
    /// <param name="x">x coord of new position</param>
    /// <param name="y">y coord of new position</param>
    /// <returns>returns false if there are collider</returns>
    private bool MoveMinotaur(float x,float y)
    {
        if (!Physics2D.OverlapCircle( new Vector3(x, y, 0f), .2f))
        {
            _movePoint.position = new Vector3(x, y, 0f);
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// detection of killing Thetheus by minotaur
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void DetectEnd(float x, float y)
    { 
        if (Vector3.Distance(this.transform.position,new Vector3(x,y,0)) < 0.4f*tileSize) 
        {
            GameOver?.Invoke();
            print("kill");
        }
    }
}
