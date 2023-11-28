using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static
{
    public static GameObject Player;
    public static PlayerController Controller;

    public static readonly int InventorySize = 3;
    public static Item[] Inventory = new Item[InventorySize];
    public static int HeldItem = 0;
}
