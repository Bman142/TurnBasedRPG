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
            PlayerManager.Instance.CurrentScene = Scene.Battle;
            PlayerManager.Instance.StartPlayerCoroutine("UpdateHealthSliders");
            foreach (Player player in PlayerManager.Instance.Players)
            {
                //player.GetComponent<SpriteRenderer>().enabled = false;
                player.Camaera.gameObject.SetActive(false);

            }

            SceneManager.LoadScene(0);
        }
    }
}