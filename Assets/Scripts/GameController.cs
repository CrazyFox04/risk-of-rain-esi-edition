using System;
using System.Runtime.InteropServices;
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
    private static extern int getAreaGuidCurrentLevel(IntPtr game, int x, int y)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int ifCanSpawnCurrentLevelSpawnAt(IntPtr game, int areaX, int areaY, int spawnId)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern const char* getType(Intptr game, int id)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterSpeed(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterJumpForce(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyFollowRange(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyAttackRange(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack(IntPtr game, int id, const char* attackName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage(IntPtr game, int id, const char* attackName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime(IntPtr game, int id, const char* attackName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterHurtTime(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isCharacterBusy(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterHealth(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getCharacterMaxHealth(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerDashForce(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getJetPackForce(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getJetPackMaxTime(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerLandingTime(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getPlayerDashTime(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime(IntPtr game, int id, const char* attackName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerDashing(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerUsingJetpack(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove(IntPtr game, int id, const char* movementName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttack(IntPtr game, int id, const char* attackName)
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isAValidId(IntPtr game, int id)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerId(IntPtr game)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack(int id, const char* attackName, int targetId)
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move(IntPtr game, int id, const char* movementName)
        
    static GameController()
    {
        Debug.Log("Plugin name: " + dllname);
    }

    void Start ()
    {
        this.game = newGame();
    }

    int GetPlayerMaxHealth()
    {
        return getPlayerMaxHealth(this.game);
    }

    int GetPlayerCurrentHealth()
    {
        return getPlayerCurrentHealth(this.game);
    }

    void AddPlayerMaxHealth(int health)
    {
        addPlayerMaxHealth(this.game, health);
    }

    void AddPlayerHealth(int health)
    {
        addPlayerHealth(this.game, health);
    }

    void TakePlayerDamage(int damage)
    {
        takePlayerDamage(this.game, damage);
    }

    int GetAreaGuidCurrentLevel(int x, int y)
    {
        return getAreaGuidCurrentLevel(this.game, x, y);
    }
    
    int IfCanSpawnCurrentLevelSpawnAt(int areaX, int areaY, int spawnId)
    {
        return ifCanSpawnCurrentLevelSpawnAt(this.game, areaX, areaY, spawnId);
    }
    
    const char* GetType(int id)
    {
        return getType(this.game, id);
    }
    
    int GetCharacterSpeed(int id)
    {
        return getCharacterSpeed(this.game, id);
    }
    
    double GetCharacterJumpForce(int id)
    {
        return getCharacterJumpForce(this.game, id);
    }
    
    double GetEnemyFollowRange(int id)
    {
        return getEnemyFollowRange(this.game, id);
    }
    
    double GetEnemyAttackRange(int id)
    {
        return getEnemyAttackRange(this.game, id);
    }
    
    bool CanCharacterAttack(int id, const char* attackName)
    {
        return canCharacterAttack(this.game, id, attackName);
    }
        
    double GetDamage(int id, const char* attackName)
    {
        return getDamage(this.game, id, attackName);
    }
        
    double GetChargeTime(int id, const char* attackName)
    {
        return getChargeTime(this.game, id, attackName);
    }
        
    double GetCharacterHurtTime(int id)
    {
        return getCharacterHurtTime(this.game, id);
    }
    
    bool IsCharacterBusy(int id)
    {
        return isCharacterBusy(this.game, id);
    }
    
    int GetCharacterHealth(int id)
    {
        return getCharacterHealth(this.game, id);
    }
    
    int GetCharacterMaxHealth(int id)
    {
        return getCharacterMaxHealth(this.game, id);
    }
    
    double GetPlayerDashForce()
    {
        return getPlayerDashForce(this.game);
    }
    
    double GetJetPackForce()
    {
        return getJetPackForce(this.game);
    }
    
    double GetJetPackMaxTime()
    {
        return getJetPackMaxTime(this.game);
    }
    
    double GetPlayerLandingTime()
    {
        return getPlayerLandingTime(this.game);
    }
    
    double GetPlayerDashTime()
    {
        return getPlayerDashTime(this.game);
    }
    
    double GetCharacterAttackTime(int id, const char* attackName)
    {
        return getCharacterAttackTime(this.game, id, attackName);
    }

    bool IsPlayerDashing()
    {
        return isPlayerDashing(this.game);
    }
    
    bool IsPlayerUsingJetpack()
    {
        return isPlayerUsingJetpack(this.game);
    }
    
    bool CanCharacterMove(int id, const char* movementName)
    {
        return canCharacterMove(this.game, id, movementName);
    }
        
    double GetCharacterCoolDownAttack(int id, const char* attackName)
    {
        return getCharacterCoolDownAttack(this.game, id, attackName);
    }
        
    bool IsAValidId(int id)
    {
        return isAValidId(this.game, id);
    }
    
    int GetPlayerId()
    {
        return getPlayerId(this.game);
    }
    
    void Attack(int id, const char* attackName, int targetId)
    {
        attack(id, attackName, targetId);
    }
    
    void Move(int id, const char* movementName)
    {
        move(this.game, id, movementName);
    }
}