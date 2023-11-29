using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Global
{
    public static GameObject Player;
    public static PlayerController Controller;

    public static readonly int InventorySize = 3;
    public static Item[] Inventory = new Item[InventorySize];
    public static int HeldItem = 0;

    public static void NextItem()
    {
        // Deactivate old current item, switch item, and set new current item's position to near player and activate.
        Inventory[HeldItem]?.gameObject.SetActive(false);
        HeldItem++;
        if (HeldItem >= InventorySize) HeldItem -= InventorySize;
        if (Inventory[HeldItem] is not null)
        {
            Inventory[HeldItem].transform.position = Player.transform.position;
            Inventory[HeldItem].gameObject.SetActive(true);
        }
    }

    public static void PrevItem()
    {
        // Deactivate old current item, switch item, and set new current item's position to near player and activate.
        Inventory[HeldItem]?.gameObject.SetActive(false);
        HeldItem--;
        if (HeldItem < 0) HeldItem += InventorySize;
        if (Inventory[HeldItem] is not null)
        {
            Inventory[HeldItem].transform.position = Player.transform.position;
            Inventory[HeldItem].gameObject.SetActive(true);
        }
    }

    public static void LoadNewLevel(Level level)
    {
        Scene currScene = SceneManager.GetActiveScene();

        string name = level switch
        {
            Level.Tutorial => "Tutorial",
            _ => throw new NotImplementedException()
        };
        SceneManager.LoadScene(name, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneByName(name);
        SceneManager.SetActiveScene(newScene);
        // MoveInventoryToScene(newScene);

        SceneManager.UnloadSceneAsync(currScene);
    }

    public static void MoveInventoryToScene(Scene scene)
    {
        foreach(Item item in Inventory)
        {
            SceneManager.MoveGameObjectToScene(item.gameObject, scene);
        }
    }
}

public enum Level
{
    Tutorial,
    One
}