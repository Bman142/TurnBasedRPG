using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.Characters;

namespace RPG.Editors
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Character character = (Character)target;

            if(GUILayout.Button("Queue Action"))
            {
                character.QueueAction(character.m_Target);
            }

            DrawDefaultInspector();

        }
    }

    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Character character = (Character)target;

            if (GUILayout.Button("Queue Action"))
            {
                character.QueueAction(character.m_Target);
            }

            DrawDefaultInspector();

        }
    }
}
