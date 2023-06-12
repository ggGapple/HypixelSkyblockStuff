using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Items
{
    //griffin pet summon item
    public class Griffin : ModItem
    {
        public override void SetStaticDefaults() //name and tooltip
        {
            DisplayName.SetDefault("Griffin Claw");
            Tooltip.SetDefault("Summons a Griffin pet that buffs the player");
        }

        public override void SetDefaults() //item data
        {
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.Griffin>(); //spawn a griffin
            Item.buffType = ModContent.BuffType<Buffs.Griffin>(); //give griffin buff
            Item.useStyle = 4; //hold up
            Item.rare = ItemRarityID.Orange; //be orange
            Item.stack = 1; //max stack is 1
            Item.useAnimation = 30; //how long to play animation
            Item.useTime = 30; //how long to actually use
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 5); //ingredients
            recipe.AddIngredient(ItemID.SoulofFlight, 5);
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddTile(TileID.MythrilAnvil); //crafting tile
            recipe.Register();
        }

        //on click
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 3600, true); //give griffin buff
            return false; //don't shoot anything
        }

    }
}