namespace Decorator.Hero
{
    public class Mage : IHero
    {
        public string Name => "Маг";
        public int Health => 80;
        public int Attack => 10;
        public int Defense => 5;
        public int MagicPower => 50;

        public string GetDescription() =>
            $"{Name} - HP:{Health}  ATK:{Attack}  DEF:{Defense}  MGK:{MagicPower}";
    }
}