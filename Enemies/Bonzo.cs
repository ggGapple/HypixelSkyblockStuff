using System;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HypixelSkyblockStuff.Items;
using Terraria.GameContent;
using HypixelSkyblockStuff.Projectiles;
using HypixelSkyblockStuff.Armor;
using Terraria.GameContent.ItemDropRules;

namespace HypixelSkyblockStuff.Enemies
{
    //bonzo boss, spawns through clown horn. Goes through three phases of shooting, standing still and spamming, and summoning; gets harder after
    //half health
    [AutoloadBossHead]
    public class Bonzo : ModNPC
    {
        public static int[] timers = new int[2]; //buncha timers
        public int AITimer = timers[0];
        public int AITimer2 = timers[1];

        public override void SetStaticDefaults() //name is bonzo
        {
            DisplayName.SetDefault("Bonzo");

        }

        public override void SetDefaults() //bonzo defaults
        {
            NPC.width = 32; //sprite width
            NPC.knockBackResist = 0.5f; //how much it resists kb
            NPC.height = 50; //sprite height
            Music = MusicID.Boss5; //music to play while it's alive
            NPC.scale = 1.5f; //to make it a little bigger
            NPC.damage = 50; //how much damage it deals (before defense)
            NPC.defense = 10; //how much defense it has
            NPC.lifeMax = 6000; //how much health it has
            NPC.boss = true; //it's a boss
            NPC.value = 40000f; //how much it drops
            NPC.aiStyle = 3; //how the default ai works, this is the default fighter ai
            AIType = NPCID.ArmoredSkeleton; //ai like armored skeleton
            NPC.HitSound = SoundID.NPCHit1; //sound on hurt
            NPC.DeathSound = SoundID.NPCDeath1; //sound on death
            Main.npcFrameCount[Type] = 14; //how many frames in the sprite sheet
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) //drops
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BonzoStaff>(), 4, 1, 1)); //1/4 chance to drop 1 bonzo staff
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BonzoMask>(), 4, 1, 1)); //1/4 chance to drop 1 bonzo mask
            npcLoot.Add(ItemDropRule.Common(ItemID.PartyBullet, 1, 50, 400)); //1/1 chance to drop 50-400 party bullets
        }
        public override void AI() //ai code
        {
            Player player = Main.player[NPC.target]; //find the player by seeing who the boss is targeting
            if (!player.active || player.dead) //if the player's not alive
            {
                NPC.TargetClosest(false); //don't target this player
                player = Main.player[NPC.target]; //target another player
                if (!player.active || player.dead) //if there was no other player
                {
                    NPC.velocity.Y -= 2; //fly up
                    if (NPC.timeLeft > 10) //despawn
                        NPC.timeLeft = 10;
                    return;
                }
            }
            AITimer++; //increment our timer, 60 times per second because this method is every tick
            
            float projectileSpeed = 12f; //velocity
            int rand = Main.rand.Next(1, 6); //random from 1-6, inclusive on 1, exclusive on 6; so 1, 2, 3, 4, or 5
            int projectileType = 0; //setting a projectile type to change later
            //changing which color balloon based on the random value
            if (rand == 1) { projectileType = ModContent.ProjectileType<ClownBalloon>(); } 
            else if (rand == 2) { projectileType = ModContent.ProjectileType<ClownBalloon2>(); }
            else if (rand == 3) { projectileType = ModContent.ProjectileType<ClownBalloon3>(); }
            else if (rand == 4) { projectileType = ModContent.ProjectileType<ClownBalloon4>(); }
            else if (rand == 5) { projectileType = ModContent.ProjectileType<ClownBalloon5>(); }
            int projectileDamage = 30; //damage of projectile
            int projectileKnockback = 2; //how much kb it deals
            var entitySource = NPC.GetSource_FromAI(); //getting our source
            Vector2 direction = player.Center - NPC.Center; //finding what direction to shoot
            direction.Normalize(); //making it a unit vector (magnitude = 1) so our velocity determines how fast it is, not the player's position
            //now a bunch of if statements. First, we check what stage it is (i.e. timer <=600 means first 10 seconds) so we can determine attack
            //phase. Then, we mod that number by something to set how often he will attack. Finally, we check if he's above or below half health,
            //and make him more powerful below half health.
            //first 10 seconds, attack every second, above half health
            if (AITimer <= 600 && AITimer % 60 == 0 && NPC.life > NPC.lifeMax / 2) 
            {
                //make a new balloon
                var df = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * projectileSpeed, projectileType, projectileDamage, projectileKnockback);
                df.hostile = true;
                df.friendly = false; //it hurts players
                df.damage = 20;

            }
            //first 10 seconds, attack every 3/4 of a second, below half health
            else if (AITimer <= 600 && AITimer % 45 == 0 && NPC.life<NPC.lifeMax/2)
            {
                var df = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * projectileSpeed, projectileType, projectileDamage, projectileKnockback);
                df.hostile = true;
                df.friendly = false;
                df.damage = 20;
            }
            //next 3.33 seconds, attack 10 times per second, below half health
            else if (600 < AITimer && AITimer < 800 && AITimer % 6 == 0 && NPC.life < NPC.lifeMax / 2)
            {

                NPC.velocity.X = 0; //stay still
                NPC.velocity.Y = 0;
                NPC.velocity = new Vector2(0, 0);
                //balloon
                var df = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * projectileSpeed, projectileType, projectileDamage, projectileKnockback);
                df.hostile = true;
                df.friendly = false;
                df.damage = 20;
            }
            //next 3.33 seconds, attack 6 times per second, above half health
            else if (600 < AITimer && AITimer < 800 && AITimer % 10 == 0&& NPC.life > NPC.lifeMax / 2)
            {

                var df = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * projectileSpeed, projectileType, projectileDamage, projectileKnockback);
                df.hostile = true;
                df.friendly = false;
                df.damage = 20;
            }
            //final 3.33 seconds, attack 2.4 times per second, below half health
            else if (AITimer > 800 && AITimer < 1000 && AITimer % 25 == 0 && NPC.life < NPC.lifeMax / 2)
            {
                //summon new goblin scout, target player
                NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.GoblinScout, 0, 0, 0, 
                    0, 0, player.whoAmI);

            }
            else if (AITimer > 800 && AITimer < 1000 && AITimer % 40 == 0 && NPC.life > NPC.lifeMax / 2)
            {
                NPC.NewNPCDirect(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, NPCID.GoblinScout, 0, 0, 0, 0, 0, player.whoAmI);
            }
            //if gone through full cycle
            else if (AITimer > 1000)
            {
                //reset timer
                AITimer = 0;
            }
            //to make sure the sprite changes direction when Bonzo goes backwards
            if (NPC.velocity.X < 0)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = -1;
            }

        }
        //draw the frames
        public override void FindFrame(int frameHeight)
        {
            //if you're moving
            if (Math.Abs(NPC.velocity.X) >= 1f)
            {
                //go to the next frame in vertical sprite sheet
                NPC.frame.Y += frameHeight;
            }
            //divide current frame offset by total frame height to get back to a frame
            NPC.frame.Y %= Main.npcFrameCount[Type]*frameHeight;
            
        }
        //draw bonzo staff on top
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //if going left
            if (NPC.spriteDirection > 0) { 
                //draw bonzo staff at center but adjust it a little so it looks natural
            spriteBatch.Draw(TextureAssets.Item[ModContent.ItemType<BonzoStaff>()].Value, NPC.Center + new Vector2(-10 * NPC.spriteDirection, 0) - 
                screenPos, null, Color.White, 1.5f, TextureAssets.Item[ModContent.ItemType<BonzoStaff>()].Value.Size() / 2, 
                -1, SpriteEffects.None, 0);
            } 
            //if going right
            else
            { //bonzo staff but other direction
                spriteBatch.Draw(TextureAssets.Item[ModContent.ItemType<BonzoStaff>()].Value, NPC.Center + new Vector2(-10 * NPC.spriteDirection, 0) - 
                    screenPos, null, Color.White, 0.1f, TextureAssets.Item[ModContent.ItemType<BonzoStaff>()].Value.Size() / 2, 1, 
                    SpriteEffects.None, 0);
            }
        
        }

    }
}
