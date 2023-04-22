using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.Managers;

namespace RPG.Editors
{
#if UNITY_EDITOR
    [CustomEditor(typeof(TurnManager))]
    public class TurnManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            

            TurnManager turnManager = (TurnManager)target;
            if (GUILayout.Button("Find Characters"))
            {
                turnManager.GetPlayers();
            }
            else if (GUILayout.Button("Calc Init"))
            {
                turnManager.CalculateInitaitve();
            }
            else if (GUILayout.Button("Clear Lists"))
            {
                turnManager.ClearLists();
            }
            else if (GUILayout.Button("Randomise Init"))
            {
                turnManager.RandomInit();
            }
            else if(GUILayout.Button("Next Turn"))
            {
                turnManager.NextTurn();
            }
            else if(GUILayout.Button("Execute Current Turn"))
            {
                turnManager.ExecuteCurrentTurn();
            }

            DrawDefaultInspector();
        }
    }
#endif
}