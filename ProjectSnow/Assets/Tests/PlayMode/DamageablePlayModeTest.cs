using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Game.DamageSystem;

public class DamageablePlayModeTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void DamageablePlayModeTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DamageablePlayModeTestWithEnumeratorPasses()
    {
        yield return new WaitForSeconds(1f);

        //Instantiating enemy and player.
        GameObject player = new GameObject("Player");
        GameObject enemy = new GameObject("Enemy");
        
        //Adding components to both player and enemy.
        Damageable dmgPlayer = player.AddComponent<Damageable>();
        Damageable dmgEnemy = enemy.AddComponent<Damageable>();

        //Creating damageInfo for the player.
        DamageInfo info = new DamageInfo();
        info.Transmitter = dmgPlayer;
        info.Damage = 5;
        
        //Initializing health for both player and enemy.
        dmgEnemy.SetHealth(10);
        dmgPlayer.SetHealth(50);
        
        //Adding shield to enemy
        dmgEnemy.Shield.SetAmount(10);
        
        //Damaging the enemy.
        dmgEnemy.DoDamage(info);
        dmgEnemy.DoDamage(info);
        dmgEnemy.DoDamage(info);
        
        Assert.AreEqual(5, dmgEnemy.CurrentHealth);
    }
}
