namespace Decorator.Weapon
{
    public class Sword : InventoryDecorator
    {
        public Sword(IHero hero) : base(hero) { }

        public override string ItemName => "Меч";
        public override string ItemDescription => "+15 ATK";
        public override int Attack => _hero.Attack + 15;
    }
}