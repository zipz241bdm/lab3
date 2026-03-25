namespace Decorator.Armor
{
    public class LeatherArmor : InventoryDecorator
    {
        public LeatherArmor(IHero hero) : base(hero) { }

        public override string ItemName => "Шкіряна броня";
        public override string ItemDescription => "+10 HP, +5 DEF";
        public override int Health => _hero.Health + 10;
        public override int Defense => _hero.Defense + 5;
    }
}