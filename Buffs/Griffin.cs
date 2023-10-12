using Terraria;
using Terraria.ModLoader;


namespace HypixelSkyblockStuff.Buffs
{
    public class Griffin : ModBuff
    {
        //corresponding buff for the Griffin pet
        public override void SetStaticDefaults() //name, tooltip, don't display time left, vanity
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
            
        }

        public override void Update(Player player, ref int buffIndex) //method run every tick
        {
            player.buffTime[buffIndex] = 18000; //keep the buff
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.Griffin>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer) //if we don't have the projectile and we've got a player
            {
                //spawn the griffin projectile
                Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 
                    0f, 0f, ModContent.ProjectileType<Projectiles.Pets.Griffin>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
            player.AddBuff(117, 100); //wrath
            player.AddBuff(2, 100); //regen
            if (player.statLife >= player.statLifeMax * 0.85) //if above 85% health
            {
                player.GetDamage(DamageClass.Generic) += 0.15f;
            }

            
        }
        
    }
}