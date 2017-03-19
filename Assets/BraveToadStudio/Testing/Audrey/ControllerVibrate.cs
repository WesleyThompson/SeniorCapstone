using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class ControllerVibrate : MonoBehaviour
{

     // Use this for initialization
     void Start()
     {

     }

     // Update is called once per frame
     public class XInputTestCS : MonoBehaviour
     {
          bool playerIndexSet = false;
          PlayerIndex playerIndex;
          GamePadState state;
          GamePadState prevState;

          // Use this for initialization
          void Start()
          {
               // No need to initialize anything for the plugin
          }

          // Update is called once per frame
          void Update()
          {
               // Find a PlayerIndex, for a single player game
               // Will find the first controller that is connected ans use it
               if (!playerIndexSet || !prevState.IsConnected)
               {
                    for (int i = 0; i < 4; ++i)
                    {
                         PlayerIndex testPlayerIndex = (PlayerIndex)i;
                         GamePadState testState = GamePad.GetState(testPlayerIndex);
                         if (testState.IsConnected)
                         {
                              Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                              playerIndex = testPlayerIndex;
                              playerIndexSet = true;
                         }
                    }
               }

               prevState = state;
               state = GamePad.GetState(playerIndex);

               // Set vibration according to triggers
               //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
               for (int i = 0; i < 3; i++)
               {
                    GamePad.SetVibration(playerIndex, 100, 100);
               }
          }
     }
}