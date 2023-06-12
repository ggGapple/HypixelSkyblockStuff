using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
	//bow that consumes mana to deal more damage on right click
	public class EndStoneBow : ModItem
	{
		public int timer; //timer
		public bool justJoined = true; //if they just joined
        public override void SetStaticDefaults() //name and tooltip
		{
			DisplayName.SetDefault("End Stone Bow"); 
			Tooltip.SetDefault("A powerful bow that allows the user to convert mana to damage by right clicking");
		}

		public override bool AltFunctionUse(Player player) //you can right click
		{

            return true;
			
		}
        public override void UpdateInventory(Player player) //runs every tick, so I just use it to increment timer lol
        {
			timer++;
            base.UpdateInventory(player);
        }

        public override void SetDefaults() //item data
		{
			Item.damage = 36; //damage
			Item.DamageType = DamageClass.Ranged; //type of damage
			Item.width = 18; //dimensions
			Item.height = 40;
			Item.useTime = 32; //how long to use
			Item.useAnimation = 32; //how long to play animation
			Item.useStyle = 5; //shoot animation
			Item.knockBack = 4; //kb
			Item.value = Item.sellPrice(0,1,50,0); //sell value
			Item.rare = 8; //text color
			Item.UseSound = SoundID.Item5; //sound to play on use
			Item.autoReuse = true; //can hold down click
			Item.useAmmo = AmmoID.Arrow; //consume arrows
			Item.shootSpeed = 6f; //how fast to shoot
			Item.shoot = ProjectileID.WoodenArrowFriendly;//what it shoots
			Item.noMelee = true; //can't melee lol
		}
		//on click
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, 
			int damage, float knockback)
        {

			position += velocity * 3; //move projectile forward a bit so it looks more natural
			//if you right click and waited 10 second cooldown or just joined
            if (player.altFunctionUse == 2 && (timer > 600||justJoined))
            {
                int extradamage = player.statMana*3; //add damage depending on mana
				player.statMana = 0; //take all the mana
				player.manaRegenDelay = 100; //slower to regen mana
				//summon new, fast and strong projectile
                int proj = Projectile.NewProjectile(source, position, velocity, ProjectileID.BoneArrow, damage + extradamage, knockback, 
					player.whoAmI);
                //this projectile does not hurt player
                Main.projectile[proj].friendly = true;
				//reset timer
				timer = 0;
				//don't shoot regular projectile
                return false;
            }
            //just shoot regular projectile
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }


        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 15); //ingredients
            recipe.AddIngredient(ItemID.ManaCrystal, 2);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.Anvils); //crafting tile
			recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Bone, 15);
            recipe2.AddIngredient(ItemID.ManaCrystal, 2);
            recipe2.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
	}
}