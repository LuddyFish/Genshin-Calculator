using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Genshin.Attributes;
using Genshin.Elements;

namespace Genshin
{
    public enum CharacterNames
    {
        Amber,
        Kaeya,
        Lisa,
        Barbara,
        Razor,
        Xiangling,
        Beidou,
        Xingqiu,
        Ningguang,
        Fischl,
        Bennett,
        Noelle,
        Chongyun,
        Sucrose,
        Jean,
        Diluc,
        Qiqi,
        Mona,
        Keqing,
        Venti,
        Klee,
    }

    public class Character
    {
        public CharacterNames Name { get; private set; }
        public Element Element { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public Weapon Weapon { get; private set; }
        public Artifact? Artifact { get; private set; }
        public int Constellations { get; private set; }
        public Stats Stats { get; private set; }

        public Character(CharacterNames name)
        {
            Name = name;
            
            var data = CharacterDataLoader.Get(name);
            Element = Enum.Parse<Element>(data.Element);
            WeaponType = Enum.Parse<WeaponType>(data.WeaponType);

            Weapon = SetDefaultWeapon();
            Artifact = null;
            Constellations = 0;

            Stats = new Stats(data.BaseHP, GetBaseATK(), data.BaseDEF);
        }

        public void LoadCharacterData()
        {
            CharacterDataLoader.Load("Data/character_data.json");
        }

        private float GetBaseATK()
        {
            return CharacterDataLoader.Get(Name).BaseATK + Weapon.BaseATK;
        }

        public void SetWeapon(WeaponNames weaponName)
        {
            Weapon = new Weapon(weaponName);

            if (Weapon.Type == WeaponType)
            {
                Console.WriteLine("Incompatible Weapon Types");
                Weapon = SetDefaultWeapon();
                return;
            }
            Stats.BaseATK = GetBaseATK();
            // Add secondary effect
        }

        public Weapon SetDefaultWeapon()
        {
            return new Weapon(
                WeaponType switch
                {
                    WeaponType.Sword => WeaponNames.Dull_Blade,
                    WeaponType.Claymore => WeaponNames.Waster_Greatsword,
                    WeaponType.Polearm => WeaponNames.Beginners_Protection,
                    WeaponType.Bow => WeaponNames.Hunters_Bow,
                    WeaponType.Catalyst => WeaponNames.Apprentices_Notes
                }
            );
        }

        public void SetArtifact()
        {

        }

        public void SetStats(int? HP, int? ATK, int? DEF, int? EM, float? ER, float? CD, float? CR, float? phys, 
            float? pyro, float? hydro, float? electro, float? cryo, float? anemo, float? geo, float? dendro,
            float? HB)
        {
            if (HP.HasValue) Stats.HP = HP.Value;
            if (ATK.HasValue) Stats.ATK = ATK.Value;
            if (DEF.HasValue) Stats.DEF = DEF.Value;
            if (EM.HasValue) Stats.Elemental_Mastery = EM.Value;
            if (ER.HasValue) Stats.Energy_Recharge = ER.Value;
            if (CD.HasValue) Stats.CRIT_DMG = CD.Value;
            if (CR.HasValue) Stats.CRIT_Rate = CR.Value;
            if (phys.HasValue) Stats.Physical_DMG_Bonus = phys.Value;
            if (pyro.HasValue) Stats.Pyro_DMG_Bonus = pyro.Value;
            if (hydro.HasValue) Stats.Hydro_DMG_Bonus = hydro.Value;
            if (electro.HasValue) Stats.Electro_DMG_Bonus = electro.Value;
            if (cryo.HasValue) Stats.Cryo_DMG_Bonus = cryo.Value;
            if (anemo.HasValue) Stats.Anemo_DMG_Bonus = anemo.Value;
            if (geo.HasValue) Stats.Geo_DMG_Bonus = geo.Value;
            if (dendro.HasValue) Stats.Dendro_DMG_Bonus = dendro.Value;
            if (HB.HasValue) Stats.Healing_Bonus = HB.Value;
        }

        public void AddStats(int? HP, int? ATK, int? DEF, int? EM, float? ER, float? CD, float? CR, float? phys,
            float? pyro, float? hydro, float? electro, float? cryo, float? anemo, float? geo, float? dendro,
            float? HB)
        {
            if (HP.HasValue) Stats.HP += HP.Value;
            if (ATK.HasValue) Stats.ATK += ATK.Value;
            if (DEF.HasValue) Stats.DEF += DEF.Value;
            if (EM.HasValue) Stats.Elemental_Mastery += EM.Value;
            if (ER.HasValue) Stats.Energy_Recharge += ER.Value;
            if (CD.HasValue) Stats.CRIT_DMG += CD.Value;
            if (CR.HasValue) Stats.CRIT_Rate += CR.Value;
            if (phys.HasValue) Stats.Physical_DMG_Bonus += phys.Value;
            if (pyro.HasValue) Stats.Pyro_DMG_Bonus += pyro.Value;
            if (hydro.HasValue) Stats.Hydro_DMG_Bonus += hydro.Value;
            if (electro.HasValue) Stats.Electro_DMG_Bonus += electro.Value;
            if (cryo.HasValue) Stats.Cryo_DMG_Bonus += cryo.Value;
            if (anemo.HasValue) Stats.Anemo_DMG_Bonus += anemo.Value;
            if (geo.HasValue) Stats.Geo_DMG_Bonus += geo.Value;
            if (dendro.HasValue) Stats.Dendro_DMG_Bonus += dendro.Value;
            if (HB.HasValue) Stats.Healing_Bonus += HB.Value;
        }

        public void ClearStats()
        {
            SetStats(HP: 0, ATK: 0, DEF: 0, EM: 0, ER: 100, CD: 50, CR: 5, phys: 0, pyro: 0, hydro: 0, electro: 0, cryo: 0, anemo: 0, geo: 0, dendro: 0, HB: 0);
        }
    }

    public class CharacterDataLoader
    {
        public class CharacterJsonData
        {
            public string Element { get; set; } = "";
            public string WeaponType { get; set; } = "";
            public float BaseHP { get; set; }
            public float BaseATK { get; set; }
            public float BaseDEF { get; set; }
        }

        private static Dictionary<CharacterNames, CharacterJsonData>? _data;

        public static void Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Character data file not found: {jsonPath}");

            string json = File.ReadAllText(jsonPath);

            var tempDict = JsonSerializer.Deserialize<Dictionary<string, CharacterJsonData>>(json)
                            ?? throw new InvalidOperationException("Failed to parse JSON.");

            _data = tempDict.ToDictionary(
                kv => Enum.Parse<CharacterNames>(kv.Key),
                kv => kv.Value
            );
        }

        public static CharacterJsonData Get(CharacterNames name)
        {
            if (_data == null)
                throw new InvalidOperationException("Character data not loaded. Call CharacterDataLoader.Load() first.");

            if (!_data.TryGetValue(name, out var info))
                throw new ArgumentException($"No character data found for {name}");

            return info;
        }
    }
}
