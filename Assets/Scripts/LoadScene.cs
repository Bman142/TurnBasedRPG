using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] int m_SceneIndex;

        public void LoadNewScene()
        {
            
            SceneManager.LoadScene(m_SceneIndex);
        }
    }
}