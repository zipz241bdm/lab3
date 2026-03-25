using Decorator.Hero;
using Decorator.Armor;
using Decorator.Weapon;
using Decorator.Artifact;

namespace Decorator
{
    class Program
    {
        static void ShowStats(IHero hero)
        {
            Console.WriteLine(hero.GetDescription());
            Console.WriteLine($"  Підсумок: HP:{hero.Health}  ATK:{hero.Attack}  " +
                              $"DEF:{hero.Defense}  MGK:{hero.MagicPower}\n");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Console.WriteLine("ВОЇН - повна бойова екіпіровка");

            IHero warrior = new Warrior();
            warrior = new PlateArmor(warrior);
            warrior = new WarAxe(warrior);
            warrior = new HealthAmulet(warrior);
            warrior = new RingOfPower(warrior);

            ShowStats(warrior);

            Console.WriteLine("МАГ - магічна екіпіровка");

            IHero mage = new Mage();
            mage = new MageRobe(mage);
            mage = new MagicStaff(mage);
            mage = new ArcaneOrb(mage);

            ShowStats(mage);

            Console.WriteLine("ПАЛАДІН - священна екіпіровка");

            IHero paladin = new Paladin();
            paladin = new PlateArmor(paladin);
            paladin = new HolyMace(paladin);
            paladin = new BlessedShield(paladin);
            paladin = new HealthAmulet(paladin);
            paladin = new RingOfPower(paladin);

            ShowStats(paladin);

            Console.WriteLine("МАГ у броні воїна (гібридна збірка)");

            IHero hybridMage = new Mage();
            hybridMage = new LeatherArmor(hybridMage);
            hybridMage = new Sword(hybridMage);
            hybridMage = new ArcaneOrb(hybridMage);

            ShowStats(hybridMage);

            Console.WriteLine("ВОЇН-ПОЧАТКІВЕЦЬ (без предметів)");

            IHero bareWarrior = new Warrior();
            ShowStats(bareWarrior);
        }
    }
}