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
        // Scene currScene = SceneManager.GetActiveScene();

        string name = level switch
        {
            Level.Tutorial => "Tutorial",
            Level.One => "Room1",
            _ => throw new NotImplementedException()
        };
        Debug.Log(name);

        for (int i = 0; i < InventorySize; i++)
        {
            Inventory[i] = null;
        }
        SceneManager.LoadScene(name, LoadSceneMode.Single);

        // Scene newScene = SceneManager.GetSceneByName(name);
        // SceneManager.SetActiveScene(newScene);
        // SceneManager.UnloadScene(currScene);

        Debug.Log(SceneManager.GetActiveScene().name);
        // MoveInventoryToScene(newScene);

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