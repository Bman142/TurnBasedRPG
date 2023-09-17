using UnityEngine;
using UnityEditor;
namespace RPG.Editors
{
#if UNITY_EDITOR
    [CustomEditor(typeof(I2toTextOnScreen))]
    public class I2toTextOnScreenEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            I2toTextOnScreen i2ToTextOnScreen = (I2toTextOnScreen)target;
            base.OnInspectorGUI();

            if (GUILayout.Button("Update"))
            {
                i2ToTextOnScreen.UpdateText();
            }

        }
    }
#endif
}