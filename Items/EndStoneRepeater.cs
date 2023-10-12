using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
	//consumes all mana to deal extra damage every shot
	public class EndStoneRepeater : ModItem
	{


		public override void SetDefaults() //item data
		{
			Item.damage = 50; //dmg
			Item.DamageType = DamageClass.Ranged; //dmg type
			Item.width = 18; //sprite dimensions
			Item.height = 40;
			Item.useTime = 15; //how long to use
			Item.useAnimation = 15; //how long animation plays 
			Item.useStyle = 5; //shoot animation
			Item.knockBack = 4; //kb
			Item.value = Item.sellPrice(0,6,0,0); //price
			Item.rare = 8; //text color
			Item.UseSound = SoundID.Item5; //bow sound
			Item.autoReuse = true; //can hold down
			Item.useAmmo = AmmoID.Arrow; //use arrows
			Item.shootSpeed = 6f; //velocity
			Item.shoot = ProjectileID.WoodenArrowFriendly; //projectile
			Item.noMelee = true; //can't melee
		}

		//upon click
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

			position += velocity * 3; //move projectile fowards a bit so it looks more natural
            int extradamage = player.statMana*5; //deal more damage depending on their mana
			player.statMana = 0; //steal their mana
			player.manaRegenDelay = 100; //make it slow to get back
			//spawn the better projectile
			int proj = Projectile.NewProjectile(source, position, velocity, ProjectileID.BoneArrow, damage + extradamage, knockback, player.whoAmI);
            Main.projectile[proj].friendly = true; //make the projectile hurt enemies not the player
			return false; //don't shoot the regular projectile
            
        }


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "EndStoneBow"); //custom mod item ingredient
			recipe.AddIngredient(ItemID.SoulofNight, 5); //regular ingredients
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil); //crafting tile
			recipe.Register();

        }
	}
}