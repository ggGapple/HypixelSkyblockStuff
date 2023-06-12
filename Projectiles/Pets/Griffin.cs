using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Projectiles.Pets
{
    //griffin pet projectile
    public class Griffin : ModProjectile
    {
        public override void SetStaticDefaults() //name, frames in sprite sheet, and is pet
        {
            DisplayName.SetDefault("Blue Whale"); 
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults() 
        {
            //follow same ai and stuff as zephyr fish cuz I don't want to code my own movement ai and zephyr fish already does exactly
            //what I want this pet to do movement wise
            Projectile.CloneDefaults(ProjectileID.ZephyrFish); 
            AIType = ProjectileID.ZephyrFish;
        }

        public override bool PreAI() //run before AI
        {
            Player player = Main.player[Projectile.owner]; //find player
            player.zephyrfish = false; //you don't actually have zephyrfish
            return true;
        }

        public override void AI() //run every tick
            { 
            Player player = Main.player[Projectile.owner]; //find our owner
            Projectile.timeLeft = 2; //stay alive
            if (!player.HasBuff(ModContent.BuffType<Buffs.Griffin>())) //if player doesn't have the blue whale buff
            {
                Projectile.timeLeft = 0; //despawn
            }
            
        }
    }
}