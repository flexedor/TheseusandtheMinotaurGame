using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed=5f;
    [SerializeField]
    private float tileSize = 1f;
    [SerializeField]
    private LayerMask LayerMaskWithColliders;
    private float _horizontalAxis = 0f;
    private float _verticalAxis = 0f;
    /// <summary>
    /// this point represents nex destination of the player, by moving it, sets destination to lerp player
    /// </summary>
    private Transform _movePoint;
   
    public static System.Action<float,float> PlayerMadeTurn;
        
    // Start is called before the first frame update
    void Awake()
    {
        _movePoint = this.gameObject.transform;
        GameController.ToursTick += ReciveButtonPressed;
        GameController.WaitWasPressed += PlayerWaits;
    }

    private void OnDestroy()
    {
        GameController.ToursTick -= ReciveButtonPressed;
        GameController.WaitWasPressed -= PlayerWaits;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _movePoint.position, moveSpeed * Time.deltaTime);
      
    }

    /// <summary>
    /// This function is invoked from game controller,checks direction, than checks is there any colliders
    /// </summary>
    private void ReciveButtonPressed()
    {
        if (!(Vector3.Distance(transform.position, _movePoint.position) <= .05f)) return;
        
        _horizontalAxis = Input.GetAxisRaw("Horizontal")*tileSize;
        _verticalAxis=Input.GetAxisRaw("Vertical")*tileSize;
        
        if (Math.Abs(_horizontalAxis)==1f*tileSize&&Math.Abs(_verticalAxis)!=1f*tileSize
            ||Math.Abs(_verticalAxis)==1f*tileSize&&Math.Abs(_horizontalAxis)!=1f*tileSize)
        {
            if (!Physics2D.OverlapCircle(_movePoint.position+new Vector3(_horizontalAxis,_verticalAxis,0f),.2f,LayerMaskWithColliders))
            {
                MovePlayer(_horizontalAxis, _verticalAxis);
            }
        }
    }

    /// <summary>
    /// Wait sequence for player
    /// </summary>
   private void PlayerWaits()
    {
        PlayerMadeTurn?.Invoke(_movePoint.position.x,_movePoint.position.y);
    }
    /// <summary>
    /// this function moves destination point of player
    /// </summary>
    /// <param name="horizontal">in witch direction horizontally move point </param>
    /// <param name="vertical">in witch direction vertically move point </param>
    void MovePlayer(float horizontal,float vertical)
    {
        var position = _movePoint.position;
        position += new Vector3(horizontal, vertical, 0f);
        _movePoint.position = position;
        PlayerMadeTurn?.Invoke(position.x,position.y);
    }
  
}
