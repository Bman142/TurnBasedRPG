using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG
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
                turnManager.GetCharacters();
            }
            else if (GUILayout.Button("Seperate Player and Enemy"))
            {
                turnManager.SeperatePlayerAndEnemy();
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

            DrawDefaultInspector();
        }
    }
#endif
}