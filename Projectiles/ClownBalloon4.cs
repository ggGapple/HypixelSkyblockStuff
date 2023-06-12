using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace HypixelSkyblockStuff.Projectiles
{
    //see ClownBalloon.cs
    public class ClownBalloon4 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clown Balloon");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.scale = 2;
            Projectile.aiStyle = 0;
            Projectile.friendly = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.damage = 30;
            Projectile.knockBack = 2;
        }
        public override void AI()
        {
            int dust = Dust.NewDust(Projectile.Center - new Vector2(8, 6), 1, 1, 16, 0f, 0f, 0, Color.Pink, 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 0.3f;
            Main.dust[dust].scale = (float)Main.rand.Next(100, 135) * 0.013f;
            
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            var e = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(0), ProjectileID.Grenade, 9, 3f, Main.myPlayer);
            e.timeLeft = 0;
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            var e = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, new Vector2(0), ProjectileID.Grenade, 9, 3f, Main.myPlayer);
            e.timeLeft = 0;
            base.OnHitNPC(target, damage, knockback, crit);
        }
    }
}
