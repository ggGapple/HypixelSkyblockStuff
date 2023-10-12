using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using HypixelSkyblockStuff.Enemies;
using Terraria.Audio;

namespace HypixelSkyblockStuff.Items
{
	//bonzo summon item
	public class ClownHorn : ModItem
	{

		public override void SetDefaults() //item data
		{
			Item.width = 32; //dimensions of sprite
			Item.height = 24;
			Item.maxStack = 20; //stack up to 20
			Item.useStyle = ItemUseStyleID.HoldUp; //how it looks to use
            Item.consumable = true; //consumes on use
            Item.UseSound = new SoundStyle($"{nameof(HypixelSkyblockStuff)}/Sounds/ClownHorn"); //what sound to play; I use a custom one
            Item.shoot = ItemID.StoneBlock; //just to tell it it can do stuff on click
			Item.rare = ItemRarityID.Purple; //make it purple
		}
		//on click
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			NPC.NewNPC(source, (int)position.X, (int)position.Y-1500, ModContent.NPCType<Bonzo>());
			//summon bonzo but like super high up so he falls on player
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(); //creating a recipe
			recipe.AddIngredient(ItemID.Bone, 20); //ingredients
            recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils); //where it's crafted
			recipe.Register();
		}
	}



}



