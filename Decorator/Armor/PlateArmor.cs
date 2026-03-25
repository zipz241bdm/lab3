namespace Decorator.Armor
{
    public class PlateArmor : InventoryDecorator
    {
        public PlateArmor(IHero hero) : base(hero) { }

        public override string ItemName => "Лицарські обладунки";
        public override string ItemDescription => "+40 HP, +20 DEF, -5 ATK";
        public override int Health => _hero.Health + 40;
        public override int Defense => _hero.Defense + 20;
        public override int Attack => _hero.Attack - 5;
    }
}