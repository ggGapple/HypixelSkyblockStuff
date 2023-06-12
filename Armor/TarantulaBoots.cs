using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    //increases move and jump speed
    internal class TarantulaBoots : ModItem
    {
        public override void SetStaticDefaults() //tooltip and name
        {

            Tooltip.SetDefault("Increases movement and jump speed");
            DisplayName.SetDefault("Tarantula Boots");
        }
        public override void SetDefaults() //item data
        {
            Item.defense = 8;
            Item.value = Item.sellPrice(0,10,0,0);
            Item.rare = 4;
            
        }
        public override void UpdateEquip(Player player) //upon equip
        {
            player.jumpSpeedBoost += 0.3f; //increasing jump speed, movespeed, and acceleration
            player.moveSpeed += 1f;
            player.accRunSpeed = 1.3f;
            player.GetModPlayer<GlobalPlayer>().TarantulaBoots = true; //activate tarantula boots field in globalplayer for dust
        }
        public override void AddRecipes() //recipes
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpiderFang,12); //ingredients
            recipe.AddIngredient(ItemID.AdamantiteBar, 15);
            recipe.AddIngredient(ItemID.Silk, 5);
            recipe.AddTile(ItemID.MythrilAnvil); //crafting tile
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.SpiderFang, 12);
            recipe2.AddIngredient(ItemID.TitaniumBar, 15);
            recipe2.AddIngredient(ItemID.Silk, 5);
            recipe2.AddTile(ItemID.MythrilAnvil);
            recipe2.Register();
        }
    }
}
