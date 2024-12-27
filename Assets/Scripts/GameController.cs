using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const string dllname = "librisk-of-rain-esi-edition-cpp";
    private IntPtr game;

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr newGame();
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void destroyGame();
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerMaxHealth(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerCurrentHealth(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void addPlayerMaxHealth(IntPtr game, int health);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void addPlayerHealth(IntPtr game, int health);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void takePlayerDamage(IntPtr game, int damage);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getAreaGuidCurrentLevel(IntPtr game, int x, int y);
    
    static GameController()
    {
        Debug.Log("Plugin name: " + dllname);
    }

    public void Start ()
    {
        this.game = newGame();
    }

    int getPlayerMaxHealth()
    {
        return getPlayerMaxHealth(this.game);
    }

    int getPlayerCurrentHealth()
    {
        return getPlayerCurrentHealth(this.game);
    }

    void addPlayerMaxHealth(int health)
    {
        addPlayerMaxHealth(this.game, health);
    }

    void addPlayerHealth(int health)
    {
        addPlayerHealth(this.game, health);
    }

    void takePlayerDamage(int damage)
    {
        takePlayerDamage(this.game, damage);
    }

    public int getAreaGuidCurrentLevel(int x, int y)
    {
        return getAreaGuidCurrentLevel(this.game, x, y);
    }
}