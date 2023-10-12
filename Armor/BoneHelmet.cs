using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace HypixelSkyblockStuff.Armor
{
    [AutoloadEquip(EquipType.Head)]
    //bone helmet is a pre-mech item that increases defense at lower health
    internal class BoneHelmet : ModItem
    {
        public override void SetDefaults() //setting item data
        {
            Item.defense = 10;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 4;
        }
        public override void UpdateEquip(Player player) //upon equip
        {
            player.GetModPlayer<GlobalPlayer>().BoneHelmet = true; //activate BoneHelmet in GlobalPlayer to actually do the scaling
            player.GetDamage(DamageClass.Generic) += 0.05f; //increase damage
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(); 
            recipe.AddIngredient(ItemID.Bone, 20); //ingredients
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddIngredient(ItemID.MythrilBar, 15);
            recipe.AddTile(TileID.MythrilAnvil); //tile
            recipe.Register(); //register the recipe
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Bone, 20);
            recipe2.AddIngredient(ItemID.SoulofNight, 5);
            recipe2.AddIngredient(ItemID.OrichalcumBar, 15);
            recipe2.AddTile(TileID.MythrilAnvil);
            recipe2.Register();
        }

        public override void UpdateArmorSet(Player player) //if you have the full set
        {
            player.setBonus = "+35% defense and +35% damage"; //tooltip
            player.statDefense*=27; //increase defense by 35%
            player.statDefense /= 20;
            player.GetDamage(DamageClass.Generic) +=0.35f; //increase damage by 35%

        }
        public override bool IsArmorSet(Item head, Item body, Item legs) //it is an armor set
        {
            //if you also have shark chestplate and tarantula boots, it is an armor set
            if (body.type == ModContent.ItemType<SharkChestplate>() && legs.type == ModContent.ItemType<TarantulaBoots>()) 
            {
                return true;
            }
            return false;
        }
    }
}
