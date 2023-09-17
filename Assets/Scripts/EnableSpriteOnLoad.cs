using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class EnableSpriteOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            foreach(Characters.Player player in Managers.PlayerManager.Instance.Players)
            {
                player.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}