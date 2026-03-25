namespace Decorator.Weapon
{
    public class WarAxe : InventoryDecorator
    {
        public WarAxe(IHero hero) : base(hero) { }

        public override string ItemName => "Бойова сокира";
        public override string ItemDescription => "+25 ATK, -10 DEF";
        public override int Attack => _hero.Attack + 25;
        public override int Defense => _hero.Defense - 10;
    }
}