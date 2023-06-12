using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
namespace HypixelSkyblockStuff.Items
{
	//post plantera sword that teleports on right click and explodes
	public class Hyperion : ModItem
	{
        public override void SetStaticDefaults() //name and tooltip
		{
			DisplayName.SetDefault("Hyperion");
			Tooltip.SetDefault("Teleports and creates a minor explosion on right click");

		}

		public override bool AltFunctionUse(Player player) //allow right click
		{

            return true;

		}
		//upon right click
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			//find where the mouse is
            var mousePos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			//if they right clicked and they have the mana and they won't go into a wall
            if (player.altFunctionUse == 2&&player.statMana >=50&& !Collision.SolidCollision(mousePos, player.width, player.height))
			{
                player.Teleport(mousePos, 4); //teleport to mouse position, use teleport animation 4
                player.velocity = new Vector2(0); //reset velocity so they don't go flying
                player.statMana -= 50; //take the mana
                int proj2 = Projectile.NewProjectile(source, Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY), 
					velocity, ProjectileID.GrenadeIII, damage, knockback, player.whoAmI); //summon a grenade at the player that explodes instantly
				Main.projectile[proj2].npcProj = true; //don't hurt players but hurt enemies
                Main.projectile[proj2].DamageType = DamageClass.Melee; //deal melee damage
				Main.projectile[proj2].damage = 80; //deal 80 damage
				Main.projectile[proj2].timeLeft = 0; //explode instantly
				
				
			}
            return false; //don't actually shoot anything
        }
        public override void SetDefaults() //item data
		{
			Item.damage = 75; //damage
			Item.DamageType = DamageClass.Melee; //type of damage
			Item.width = 44; //sprite dimensions
			Item.height = 48;
			Item.useTime = 20; //how long to use
			Item.useAnimation = 20; //how long animation plays
			Item.useStyle = 1; //sword swing
			Item.knockBack = 4; //kb
			Item.value = Item.sellPrice(0,15,50,0); //how much it sells for
			Item.rare = 9; //text color
			Item.UseSound = SoundID.Item1; //sound that plays on use
			Item.autoReuse = true; //can hold down
            Item.shoot = ProjectileID.WoodenArrowFriendly; //you can shoot
        }



		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null,"AspectOfTheEnd"); //custom mod item ingredient
			recipe.AddIngredient(ItemID.RodofDiscord, 1); //ingredient
			recipe.AddIngredient(ItemID.GrenadeLauncher, 1);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 15);
            recipe.AddTile(TileID.MythrilAnvil); //crafting tile
			recipe.Register();
		}
	}



}



