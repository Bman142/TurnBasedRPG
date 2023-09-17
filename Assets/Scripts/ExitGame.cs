using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG
{
    public class ExitGame : MonoBehaviour
    {
        public void Exit()
        {            
            Debug.Log("Quit the Game!");
            Application.Quit(0);
        }
    }
}