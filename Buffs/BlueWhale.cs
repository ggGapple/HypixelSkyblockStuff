using Terraria;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Buffs
{
    //corresponding buff for the blue whale pet
    public class BlueWhale : ModBuff
    {
        public override void SetStaticDefaults() //name, tooltip, don't show duration of buff left, vanity pet
        {
            DisplayName.SetDefault("Blue Whale");
            Description.SetDefault("Permanent lifeforce and night vision, gives defense depending on how much health you have");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
            
        }

        public override void Update(Player player, ref int buffIndex) //method that does stuff every tick
        {
            player.buffTime[buffIndex] = 18000; //keep it from going away
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.BlueWhale>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) //if we don't have the projectile and we've got a player
            {
                //spawn the blue whale projectile
                Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), 
                    player.position.Y + (float)(player.height / 2), 0f, 0f, 
                    ModContent.ProjectileType<Projectiles.Pets.BlueWhale>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
            player.AddBuff(113,100); //lifeforce
            player.statDefense += player.statLife / 22; //give more defense depending on health
            
        }
        
    }
}