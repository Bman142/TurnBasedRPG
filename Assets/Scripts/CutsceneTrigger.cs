using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] TextLooper cutsceneTarget;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            cutsceneTarget.Typewriter("A deal worth breaking is the type you break when going to war", 0.1f, 5f);
        }
    }
}