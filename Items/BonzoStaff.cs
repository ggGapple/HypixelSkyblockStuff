using Microsoft.Xna.Framework;
using HypixelSkyblockStuff.Projectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
	//magic weapon that shoots exploding balloons
	public class BonzoStaff : ModItem
	{
        public override void SetStaticDefaults() //name, tooltip, and is a staff
		{
			Item.staff[Item.type] = true;
		}


		public override void SetDefaults()
		{
			Item.damage = 30; //dmg
			Item.DamageType = DamageClass.Magic; //what type of dmg
			Item.width = 42; //dimensions of sprite
			Item.height = 42;
			Item.mana = 12; //mana cost
			Item.useTime = 20; //how quick it is
			Item.useAnimation = 20; //how quick the animation plays
			Item.useStyle = 5; //how it looks to use
			Item.knockBack = 4; //kb
			Item.value = 15000; //how much it sells for
			Item.rare = 11; //color of name
			Item.UseSound = SoundID.Item20; //sound that plays on swing
			Item.autoReuse = true; //can hold down click
			Item.shoot = ModContent.ProjectileType<ClownBalloon>(); //what it spawns (we won't be using this, we'll summon it ourselves)
			Item.shootSpeed = 8f;  //how fast it shoots
			Item.noMelee = true; //so you can't melee with this lol
		}
		//on use
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			//get a random number: 1, 2, 3, 4, or 5
			int rand = Main.rand.Next(1, 6);
			Vector2 offset = velocity * 3 ; //make it go a little more in front so it looks more clean
			position += offset;
			//change balloon color depoending on random number (all clown balloons are custom, diff colors)
			if (rand == 1) { 
                type = ModContent.ProjectileType<ClownBalloon>();
            }
			else if (rand == 2) {
                type = ModContent.ProjectileType<ClownBalloon2>();
            }
			else if (rand == 3) {
                type = ModContent.ProjectileType<ClownBalloon3>();
            }
			else if (rand == 4) {
                type = ModContent.ProjectileType<ClownBalloon4>();
            }
			else if (rand == 5) {
                type= ModContent.ProjectileType<ClownBalloon5>();
            }
			//make a new projectile, from player, using given velocity, damage, kb, and the balloon color we chose earlier
			var e =Projectile.NewProjectileDirect(source,position,velocity*1.5f,type,damage,knockback,Main.myPlayer);
			e.hostile = false;
			e.friendly = true; //hurt enemies
			return false; //don't shoot the regular projectile

        }



        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(); //creating a recipe
			recipe.AddIngredient(ItemID.Diamond, 10); //ingredients
            recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.DemonAltar); //where it's crafted
			recipe.Register();
		}
	}



}



