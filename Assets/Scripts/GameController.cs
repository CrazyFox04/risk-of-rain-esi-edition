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
    private static extern double getCharacterSpeed(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterJumpForce(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyFollowRange(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getEnemyAttackRange(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK1(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK2(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK3(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK4(IntPtr game, int id); 
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK5(IntPtr game, int id); 
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK_SPECTRUM(IntPtr game, int id); 
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK_MONSTER(IntPtr game, int id); 
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterAttack_ATTACK_DROID(IntPtr game, int id); 
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK1(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK2(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK3(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK4(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK5(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK_SPECTRUM(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK1_MONSTER(IntPtr game, int id);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getDamage_ATTACK1_DROID(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK1(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK2(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK3(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK4(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK5(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK_SPECTRUM(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK_MONSTER(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getChargeTime_ATTACK_DROID(IntPtr game, int id);

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
    private static extern double getCharacterAttackTime_ATTACK1(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK2(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK3(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK4(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK5(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK_SPECTRUM(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK_MONSTER(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterAttackTime_ATTACK_DROID(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerDashing(IntPtr game);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isPlayerUsingJetpack(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove_RUN(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove_JUMP(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool canCharacterMove_DASH(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK1(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK2(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK3(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK4(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK5(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK_SPECTRUM(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK_MONTER(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern double getCharacterCoolDownAttackTime_ATTACK_DROID(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern bool isAValidId(IntPtr game, int id);

    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern int getPlayerId(IntPtr game);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK1(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK2(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK3(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK4(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK5(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK_SPECTRUM(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK_MONSTER(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void attack_ATTACK_DROID(int id, int targetId);
    
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move_RUN(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move_JUMP(IntPtr game, int id);
        
    [DllImport(dllname, CallingConvention = CallingConvention.Cdecl)]
    private static extern void move_DASH(IntPtr game, int id);
        
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
    
    public bool CanCharacterAttack_ATTACK1(int id)
    {
        return canCharacterAttack_ATTACK1(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK2(int id)
    {
        return canCharacterAttack_ATTACK2(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK3(int id)
    {
        return canCharacterAttack_ATTACK3(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK4(int id)
    {
        return canCharacterAttack_ATTACK4(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK5(int id)
    {
        return canCharacterAttack_ATTACK5(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK_SPECTRUM(int id)
    {
        return canCharacterAttack_ATTACK_SPECTRUM(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK_MONSTER(int id)
    {
        return canCharacterAttack_ATTACK_MONSTER(this.game, id);
    }
    
    public bool CanCharacterAttack_ATTACK_DROID(int id)
    {
        return canCharacterAttack_ATTACK_DROID(this.game, id);
    }
        
    public double GetDamage_ATTACK1(int id)
    {
        return getDamage_ATTACK1(this.game, id);
    }
    
    public double GetDamage_ATTACK2(int id)
    {
        return getDamage_ATTACK2(this.game, id);
    }
    
    public double GetDamage_ATTACK3(int id)
    {
        return getDamage_ATTACK3(this.game, id);
    }
    
    public double GetDamage_ATTACK4(int id)
    {
        return getDamage_ATTACK4(this.game, id);
    }
    
    public double GetDamage_ATTACK5(int id)
    {
        return getDamage_ATTACK5(this.game, id);
    }
    
    public double GetDamage_ATTACK_SPECTRUM(int id)
    {
        return getDamage_ATTACK_SPECTRUM(this.game, id);
    }
    
    public double GetDamage_ATTACK1_MONSTER(int id)
    {
        return getDamage_ATTACK1_MONSTER(this.game, id);
    }
    
    public double GetDamage_ATTACK1_DROID(int id)
    {
        return getDamage_ATTACK1_DROID(this.game, id);
    }
        
    public double GetChargeTime_ATTACK1(int id)
    {
        return getChargeTime_ATTACK1(this.game, ide);
    }
    
    public double GetChargeTime_ATTACK2(int id)
    {
        return getChargeTime_ATTACK2(this.game, id);
    }
    
    public double GetChargeTime_ATTACK3(int id)
    {
        return getChargeTime_ATTACK3(this.game, id);
    }
    
    public double GetChargeTime_ATTACK4(int id)
    {
        return getChargeTime_ATTACK4(this.game, id);
    }
    
    public double GetChargeTime_ATTACK5(int id)
    {
        return getChargeTime_ATTACK5(this.game, id);
    }
    
    public double GetChargeTime_ATTACK_SPECTRUM(int id)
    {
        return getChargeTime_ATTACK_SPECTRUM(this.game, id);
    }
    
    public double GetChargeTime_ATTACK_MONSTER(int id)
    {
        return getChargeTime_ATTACK_MONSTER(this.game, id);
    }
    
    public double GetChargeTime_ATTACK_DROID(int id)
    {
        return getChargeTime_ATTACK_DROID(this.game, id);
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
    
    public double GetCharacterAttackTime_ATTACK1(int id)
    {
        return getCharacterAttackTime_ATTACK1(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK2(int id)
    {
        return getCharacterAttackTime_ATTACK2(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK3(int id)
    {
        return getCharacterAttackTime_ATTACK3(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK4(int id)
    {
        return getCharacterAttackTime_ATTACK4(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK5(int id)
    {
        return getCharacterAttackTime_ATTACK5(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK_SPECTRUM(int id)
    {
        return getCharacterAttackTime_ATTACK_SPECTRUM(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK_MONSTER(int id)
    {
        return getCharacterAttackTime_ATTACK_MONSTER(this.game, id);
    }
    
    public double GetCharacterAttackTime_ATTACK_DROID(int id)
    {
        return getCharacterAttackTime_ATTACK_DROID(this.game, id);
    }

    public bool IsPlayerDashing()
    {
        return isPlayerDashing(this.game);
    }
    
    public bool IsPlayerUsingJetpack()
    {
        return isPlayerUsingJetpack(this.game);
    }
    
    public bool CanCharacterMove_RUN(int id)
    {
        return canCharacterMove_RUN(this.game, id);
    }
    
    public bool CanCharacterMove_JUMP(int id)
    {
        return canCharacterMove_JUMP(this.game, id);
    }
    
    public bool CanCharacterMove_DASH(int id)
    {
        return canCharacterMove_DASH(this.game, id);
    }
        
    public double GetCharacterCoolDownAttack_ATTACK1(int id)
    {
        return getCharacterCoolDownAttack_ATTACK1(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK2(int id)
    {
        return getCharacterCoolDownAttack_ATTACK2(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK3(int id)
    {
        return getCharacterCoolDownAttack_ATTACK3(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK4(int id)
    {
        return getCharacterCoolDownAttack_ATTACK4(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK5(int id)
    {
        return getCharacterCoolDownAttack_ATTACK5(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK_SPECTRUM(int id)
    {
        return getCharacterCoolDownAttack_ATTACK_SPECTRUM(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK_MONSTER(int id)
    {
        return getCharacterCoolDownAttack_ATTACK_MONSTER(this.game, id);
    }
    
    public double GetCharacterCoolDownAttack_ATTACK_DROID(int id)
    {
        return getCharacterCoolDownAttack_ATTACK_DROID(this.game, id);
    }
        
    public bool IsAValidId(int id)
    {
        return isAValidId(this.game, id);
    }
    
    public int GetPlayerId()
    {
        return getPlayerId(this.game);
    }
    
    public void Attack_ATTACK1(int id, int targetId)
    {
        attack_ATTACK1(id, attackName, targetId);
    }
    
    public void Attack_ATTACK2(int id, int targetId)
    {
        attack_ATTACK2(id, attackName, targetId);
    }
    
    public void Attack_ATTACK3(int id, int targetId)
    {
        attack_ATTACK3(id, attackName, targetId);
    }
    
    public void Attack_ATTACK4(int id, int targetId)
    {
        attack_ATTACK4(id, attackName, targetId);
    }
    
    public void Attack_ATTACK5(int id, int targetId)
    {
        attack_ATTACK5(id, attackName, targetId);
    }
    
    public void Attack_ATTACK_SPECTRUM(int id, int targetId)
    {
        attack_ATTACK_SPECTRUM(id, attackName, targetId);
    }
    
    public void Attack_ATTACK_MONSTER(int id, int targetId)
    {
        attack_ATTACK_MONSTER(id, attackName, targetId);
    }
    
    public void Attack_ATTACK_DROID(int id, int targetId)
    {
        attack_ATTACK_DROID(id, attackName, targetId);
    }
    
    public void Move_RUN(int id)
    {
        move_RUN(this.game, id);
    }
    
    public void Move_JUMP(int id)
    {
        move_JUMP(this.game, id);
    }
    
    public void Move_DASH(int id)
    {
        move_DASH(this.game, id);
    }
}
