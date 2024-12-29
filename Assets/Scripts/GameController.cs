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
    private static extern int getCharacterType(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterSpeed(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterJumpForce(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyFollowRange(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyAttackRange(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack(IntPtr game, int id, int attackIndex);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getAttackDamage(IntPtr game, int id, int attackIndex);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getAttackChargeTime(IntPtr game, int id, int attackIndex);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterHurtTime(IntPtr game, int id);

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
    private static extern double getCharacterAttackTime(IntPtr game, int id, int attackIndex);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerDashing(IntPtr game);

    // [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    // private static extern bool isPlayerUsingJetpack(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove(IntPtr game, int id, int moveIndex);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime(IntPtr game, int id, int attackIndex);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isAValidId(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerId(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack(IntPtr game, int id, int attackIndex, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move(IntPtr game, int id, int moveIndex);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isCharacterBusy(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isCharacterOnGround(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void landCharacter(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void takeOffCharacter(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int isMoving(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void stopMoving(IntPtr game, int id, int moveIndex);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int activateBossSpawn(IntPtr game, int areaX, int areaY, int spawnId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canActivateBossSpawn(IntPtr game, int areaX, int areaY, int spawnId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownMovementTime(IntPtr game, int id, int moveIndex);
    
        
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
    
    public int GetCharacterType(int id)
    {
        return getCharacterType(this.game, id);
    }
    
    public double GetCharacterSpeed(int id)
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
    
    public bool CanCharacterAttack(int id, int attackIndex)
    {
        return canCharacterAttack(this.game, id, attackIndex);
    }
        
    public double GetAttackDamage(int id, int attackIndex)
    {
        return getAttackDamage(this.game, id, attackIndex);
    }
    
    public double GetAttackChargeTime(int id, int attackIndex)
    {
        return getAttackChargeTime(this.game, id, attackIndex);
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
    
    public double GetCharacterAttackTime(int id, int attackIndex)
    {
        return getCharacterAttackTime(this.game, id, attackIndex);
    }

    public bool IsPlayerDashing()
    {
        return isPlayerDashing(this.game);
    }
    
    // public bool IsPlayerUsingJetpack()
    // {
    //     return isPlayerUsingJetpack(this.game);
    // }
    
    public bool CanCharacterMove(int id, int moveIndex)
    {
        return canCharacterMove(this.game, id, moveIndex);
    }

    public double GetCharacterCoolDownAttackTime(int id, int attackIndex)
    {
        return getCharacterCoolDownAttackTime(this.game, id, attackIndex);
    }
        
    public bool IsAValidId(int id)
    {
        return isAValidId(this.game, id);
    }
    
    public int GetPlayerId()
    {
        return getPlayerId(this.game);
    }
    
    public void Attack(int id, int attackIndex, int targetId)
    {
        attack(this.game, id, attackIndex, targetId);
    }
    
   public void Move(int id, int moveName)
    {
        move(this.game, id, moveName);
    }
   
    public void StopMoving(int id, int moveName)
    {
        stopMoving(this.game, id, moveName);
    } 
   
    public void LandCharacter(int id)
    {
        landCharacter(this.game, id);
    }
    
    public void TakeOffCharacter(int id)
    {
        takeOffCharacter(this.game, id);
    }
    
    public int IsMoving(int id)
    {
        return isMoving(this.game, id);
    }
    
    public bool IsCharacterOnGround(int id)
    {
        return isCharacterOnGround(this.game, id);
    }
    
    public int ActivateBossSpawn(int areaX, int areaY, int spawnId)
    {
        return activateBossSpawn(this.game, areaX, areaY, spawnId);
    }
    
    public bool CanActivateBossSpawn(int areaX, int areaY, int spawnId)
    {
        return canActivateBossSpawn(this.game, areaX, areaY, spawnId);
    }
    
    public double GetCharacterCoolDownMovementTime(int id, int moveIndex)
    {
        return getCharacterCoolDownMovementTime(this.game, id, moveIndex);
    }
}