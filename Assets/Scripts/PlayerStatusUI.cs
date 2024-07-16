using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    public Player player;
    public Text hpText;
    public Text goldText;

    // Update is called once per frame
    void Update()
    {
        hpText.text = $"HP:{player.hp}";
        goldText.text = $"GOLD:{player.gold}";
    }
}
