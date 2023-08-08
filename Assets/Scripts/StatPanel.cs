using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;
namespace RPG
{

    public class StatPanel : MonoBehaviour
    {
        Characters.Player Player;
        [SerializeField] TMP_Text Name, Health, Compat, DEF, ACC, EVA, DEX, STR;
        private void OnEnable()
        {
            Player = Managers.PlayerManager.Instance.OverworldPlayer;
            Name.text = LocalizationManager.GetTranslation("Menu/Name") + " : " + Player.Name;
            Health.text = LocalizationManager.GetTranslation("Menu/Health") + " : " + Player.Health + " / " + Player.MaxHealth;
            Compat.text = LocalizationManager.GetTranslation("Menu/Compatibility") + " : " + Player.MagicPoint + " / " + Player.MaxMagicPoint;
            DEF.text = "DEF: " + Player.Defense;
            ACC.text = "ACC: " + Player.Accuracy;
            EVA.text = "EVA: " + Player.Evasion;
            DEX.text = "DEX: " + Player.Dexterity;
            STR.text = "STR: " + Player.Strength;
        }
    }
}
