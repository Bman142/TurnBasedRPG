using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG
{
    public class TextLooper : MonoBehaviour
    {
        [SerializeField] TMP_Text TextObject;
        bool active;
        public void Typewriter(string inputString, float delay, float fadeDelay)
        {
            if (active){ return; }
            
            StartCoroutine(LoopText(inputString, delay, fadeDelay));
        }

        private IEnumerator LoopText(string inputString, float delay, float fadeDelay)
        {
            active = !active;
            Managers.PlayerManager.Instance.OverworldPlayer.GetComponent<GameControls>().InventoryOpen = true;
            TextObject.alpha = 1;
            foreach(char character in inputString)
            {
                TextObject.text += character;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(fadeDelay);
            for(float i = 1; i>=0; i = i - 0.01f)
            {
                TextObject.alpha = i;
                yield return new WaitForSeconds(0.01f);
            }
            TextObject.text = "";
            active = !active;
            Managers.PlayerManager.Instance.OverworldPlayer.GetComponent<GameControls>().InventoryOpen = false;
        }
    }
}