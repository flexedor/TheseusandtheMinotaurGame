using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
   /// <summary>
   /// This script is responding for checking win sequence  
   /// </summary>
   public static System.Action WinSequence;
   [SerializeField] private float LengthToFinish=0.5f;
   private void OnEnable()
   {
      PlayerController.PlayerMadeTurn += CheckIfWin;
      
   }

   private void OnDisable()
   {
      PlayerController.PlayerMadeTurn -= CheckIfWin;
   }
   /// <summary>
   /// this function checks Win sequence and if it is, function will invoke win action in game contoller 
   /// </summary>
   /// <param name="x"> x coordinates of player</param>
   /// <param name="y"> y coordinates of player</param>
   private void CheckIfWin(float x,float y )
   {
      if (Vector3.Distance(this.transform.position,new Vector3(x,y,0))<LengthToFinish)
      {
         WinSequence?.Invoke();
      }
   }
}
