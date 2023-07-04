using RPG.Characters;
using RPG.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG
{

    public class BattleStart : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.GetComponentInChildren<Camera>().gameObject.SetActive(false);

            PlayerManager.Instance.LoadScene(0, Scene.Battle);
        }
    }
}