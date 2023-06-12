using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff
{
    //class that does a bunch of things relating to the player, 
    public class GlobalPlayer : ModPlayer
    {
        public bool TarantulaBoots = false; //setting custom item variables
        public bool SharkChestplate = false;
        public bool sharkBarrier = false;
        public bool BoneHelmet = false;
        public bool BonzoMask = false;
        public bool justJoined = true; //if they just joined the world, to bypass cooldown
        public int timer; //timers
        public int timer2;
        public override void PostUpdate() //run every tick
        {
            timer2++; //increment one of the timers
            //if the player is moving quickly (either up or down) and has tarantula boots equipped
            if ((Math.Abs(Player.velocity.X) > 7 || Math.Abs(Player.velocity.Y) > 7) && TarantulaBoots)
            {
                //spawn new red dust at player's feet
                int dust = Dust.NewDust(Player.position + new Vector2(0, Player.height - 4), Player.width, 4, 16, 0f, 0f, 
                    0, Colors.RarityDarkRed, 1f);
                Main.dust[dust].noGravity = true; //don't have gravity
            }
            //if player has shark chestplate on and is at full health and has waited cooldown or just joined/put it on
            if (SharkChestplate && Player.statLife == Player.statLifeMax && (timer2 > 1800 || justJoined))
            {
                sharkBarrier = true; //give the player the shark variable
                justJoined = false; //they didn't just join now
            }
            if (sharkBarrier && Player.statLife < Player.statLifeMax) //if they have the shark barrier and took damage (they aren't at full health)
            {
                SoundEngine.PlaySound(SoundID.Item14, Player.position); //play the sound
                Player.statLife = Player.statLifeMax; //get them back to full health
                sharkBarrier = false; //remove shark barrier
                timer2 = 0; //reset timer, begin cooldown
            }

            if (BoneHelmet) //if the player has the bone helmet on
            {
                //give them more defense depending on the difference between max health and current health
                Player.statDefense += (Player.statLifeMax - Player.statLife) / 20; 
            }
            if (timer > 0) //if the timer is above 0
            {
                timer--; //decrement it
            }


        }
        public override void ResetEffects() //if they take off the armor piece, reset it
        {
            TarantulaBoots = false;
            SharkChestplate = false;
            BoneHelmet = false;
            BonzoMask = false;
            sharkBarrier = false;
        }
        //activates right when the player dies, and determines if the player does. If returned false, the player doesn't die and lives
        //the hit, and if returned true, the player dies as regular
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (BonzoMask && timer == 0) //if player has bonzo mask on, and cooldown
            {
                Player.statLife = Player.statLifeMax; //get the player back to full health
                Player.HealEffect(1); //do the heal number effect
                Player.immune = true; //give invulnerability frames
                Player.immuneTime = 60; //1 second worth of i-frames
                SoundEngine.PlaySound(SoundID.Item6, Player.position); //play teleport sound
                timer = 3600; //set the timer bigger
                return false; //don't kill the player
            }
            //otherwise just kill them as normal
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }
        //sets weapon damage
        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (timer > 3300) damage += 0.5f; //if within 5 seconds of Bonzo mask activating, increase damage
            base.ModifyWeaponDamage(item, ref damage);
        }
    }
}