using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace HypixelSkyblockStuff.Projectiles
{
    //custom projectile shot by bonzo and bonzo staff
    //NOTE: I am only going to comment this one file; the other 4 clown balloons are exact replicas of this, just different colors of dust and have
    //corresponding color sprites
    public class ClownBalloon : ModProjectile
    {
        public override void SetStaticDefaults() //name of projectile
        {
            DisplayName.SetDefault("Clown Balloon");
        }
        public override void SetDefaults() //item data
        {
            Projectile.DamageType = DamageClass.Magic; //type of damage
            Projectile.width = 8; //dimensions of sprite
            Projectile.height = 8;
            Projectile.scale = 2; //be 2x bigger than sprite
            Projectile.aiStyle = 0; //ai type of projectile; 0 is bullet, so don't feel gravity
            Projectile.penetrate = 1; //only hit 1 enemy
            Projectile.timeLeft = 600; //how long it exists for
            Projectile.light = 0.25f; //how much light it emits
            Projectile.ignoreWater = true; //go straight through water
            Projectile.tileCollide = true; //hit blocks
            Projectile.damage = 30; //dmg
            Projectile.knockBack = 2; //kb
        }
        public override void AI() //run every tick
        {
            //spawn orange dust near center
            int dust = Dust.NewDust(Projectile.Center-new Vector2(8,6), 1, 1, 16, 0f, 0f, 0, Color.Orange, 1f);
            Main.dust[dust].noGravity = true; //dust isn't affected by gravity
            Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f; //vary in size
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity) //if you hit tile
        {
            //spawn explosion
            var e = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(0), ProjectileID.Grenade, 
                9, 3f, Main.myPlayer);
            e.timeLeft = 0; //explode instantly
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) //if you hit npc
        {
            //spawn explosion
            var e = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(0), 
                ProjectileID.Grenade, 9, 3f, Main.myPlayer);
            e.timeLeft = 0; //explode instantly
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
