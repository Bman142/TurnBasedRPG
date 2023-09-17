using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;
using UnityEngine.UI;

namespace RPG
{
    public class I2toTextOnScreen : MonoBehaviour
    {
        [SerializeField] LocalizedString Text;
        [SerializeField] TMP_Text textAsset;


        public void UpdateText()
        {
            if(textAsset == null)
            {
                Debug.LogError("Text Asset not Set to Object", this.gameObject);
                return;
            }
            textAsset.text = Text;
        }
    }
}