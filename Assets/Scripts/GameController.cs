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
    private static extern void takePlayerDamage(IntPtr game, int damage);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getAreaGuidCurrentLevel(IntPtr game, int x, int y);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int ifCanSpawnCurrentLevelSpawnAt(IntPtr game, int areaX, int areaY, int spawnId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern string getType(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterSpeed(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterJumpForce(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyFollowRange(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyAttackRange(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack(IntPtr game, int id, string attackName);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage(IntPtr game, int id, string attackName);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime(IntPtr game, int id, string attackName);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterHurtTime(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isCharacterBusy(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterHealth(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterMaxHealth(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerDashForce(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getJetPackForce(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getJetPackMaxTime(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerLandingTime(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerDashTime(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime(IntPtr game, int id, string attackName);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerDashing(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerUsingJetpack(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove(IntPtr game, int id, string movementName);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttack(IntPtr game, int id, string attackName);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isAValidId(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerId(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack(int id, string attackName, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move(IntPtr game, int id, string movementName);
        
    static GameController()
    {
        Debug.Log("Plugin name: " + dllname);
    }

    public void Start ()
    {
        this.game = newGame();
    }

    public int GetPlayerMaxHealth()
    {
        return getPlayerMaxHealth(this.game);
    }

    public int GetPlayerCurrentHealth()
    {
        return getPlayerCurrentHealth(this.game);
    }

    public void TakePlayerDamage(int damage)
    {
        takePlayerDamage(this.game, damage);
    }

    public int GetAreaGuidCurrentLevel(int x, int y)
    {
        return getAreaGuidCurrentLevel(this.game, x, y);
    }
    
    public int IfCanSpawnCurrentLevelSpawnAt(int areaX, int areaY, int spawnId)
    {
        return ifCanSpawnCurrentLevelSpawnAt(this.game, areaX, areaY, spawnId);
    }
    
    public string GetType(int id)
    {
        return getType(this.game, id);
    }
    
    public int GetCharacterSpeed(int id)
    {
        return getCharacterSpeed(this.game, id);
    }
    
    public double GetCharacterJumpForce(int id)
    {
        return getCharacterJumpForce(this.game, id);
    }
    
    public double GetEnemyFollowRange(int id)
    {
        return getEnemyFollowRange(this.game, id);
    }
    
    public double GetEnemyAttackRange(int id)
    {
        return getEnemyAttackRange(this.game, id);
    }
    
    public bool CanCharacterAttack(int id, string attackName)
    {
        return canCharacterAttack(this.game, id, attackName);
    }
        
    public double GetDamage(int id, string attackName)
    {
        return getDamage(this.game, id, attackName);
    }
        
    public double GetChargeTime(int id, string attackName)
    {
        return getChargeTime(this.game, id, attackName);
    }
        
    public double GetCharacterHurtTime(int id)
    {
        return getCharacterHurtTime(this.game, id);
    }
    
    public bool IsCharacterBusy(int id)
    {
        return isCharacterBusy(this.game, id);
    }
    
    public int GetCharacterHealth(int id)
    {
        return getCharacterHealth(this.game, id);
    }
    
    public int GetCharacterMaxHealth(int id)
    {
        return getCharacterMaxHealth(this.game, id);
    }
    
    public double GetPlayerDashForce()
    {
        return getPlayerDashForce(this.game);
    }
    
    public double GetJetPackForce()
    {
        return getJetPackForce(this.game);
    }
    
    public double GetJetPackMaxTime()
    {
        return getJetPackMaxTime(this.game);
    }
    
    public double GetPlayerLandingTime()
    {
        return getPlayerLandingTime(this.game);
    }
    
    public double GetPlayerDashTime()
    {
        return getPlayerDashTime(this.game);
    }
    
    public double GetCharacterAttackTime(int id, string attackName)
    {
        return getCharacterAttackTime(this.game, id, attackName);
    }

    public bool IsPlayerDashing()
    {
        return isPlayerDashing(this.game);
    }
    
    public bool IsPlayerUsingJetpack()
    {
        return isPlayerUsingJetpack(this.game);
    }
    
    public bool CanCharacterMove(int id, string movementName)
    {
        return canCharacterMove(this.game, id, movementName);
    }
        
    public double GetCharacterCoolDownAttack(int id, string attackName)
    {
        return getCharacterCoolDownAttack(this.game, id, attackName);
    }
        
    public bool IsAValidId(int id)
    {
        return isAValidId(this.game, id);
    }
    
    public int GetPlayerId()
    {
        return getPlayerId(this.game);
    }
    
    public void Attack(int id, string attackName, int targetId)
    {
        attack(id, attackName, targetId);
    }
    
    public void Move(int id, string movementName)
    {
        move(this.game, id, movementName);
    }
}
