using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    public static Player player;

    public static void ChangeMoney(int money)
    {
        player.money = money;
    }

    public static int GetMoney()
    {
        return player.money;
    }
}
