using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
	//item that teleports on right click, 5 sec cooldown and costs mana
	public class AspectOfTheEnd : ModItem
	{
		public int timer; //timer
		public bool justJoinedWorld = true; //if they just joined, this variable bypasses the cooldown
        public override void UpdateInventory(Player player) //I just use this method because it runs every tick, so I can update my timer
        {
			timer++; //increment timer
            base.UpdateInventory(player);
        }

        public override bool AltFunctionUse(Player player) //on right click
		{
            var mousePos = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY); //finding mouse position


			//if player has the mana and won't teleport into wall and has waited cooldown OR just joined world
            if (player.statMana >= 100&& !Collision.SolidCollision(mousePos, player.width, player.height) && (timer > 300||justJoinedWorld))
			{
				player.Teleport(mousePos,4); //teleport player to mouse position
				justJoinedWorld = false; //so that if they did just join the world, they have to wait cooldown after using
				player.statMana -= 100; //taking mana
                player.velocity = new Vector2(0); //resetting velocity so they don't go flying downwards
				timer = 0; //resetting timer
            }

            return true;

		}

		public override void SetDefaults()
		{ 
			Item.damage = 24; //dmg
			Item.DamageType = DamageClass.Melee; //what type of dmg
			Item.width = 46; //dimensions of sprite
			Item.height = 54;
			Item.useTime = 20; //how quick it is
			Item.useAnimation = 20; //how quick the animation plays
			Item.useStyle = 1; //how it looks to use
			Item.knockBack = 4; //kb
			Item.value = 25000; //how much it sells for
			Item.rare = 11; //color of text
			Item.UseSound = SoundID.Item1; //sound that plays on swing
			Item.autoReuse = true; //can hold down click
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



