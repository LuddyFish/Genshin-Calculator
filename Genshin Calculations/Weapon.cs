using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Genshin.CharacterDataLoader;

namespace Genshin
{
    public enum WeaponType
    {
        Sword,
        Claymore,
        Polearm,
        Bow,
        Catalyst
    }

    public enum WeaponNames
    {
        // 1 star
        Apprentices_Notes,
        Beginners_Protection,
        Dull_Blade,
        Hunters_Bow,
        Waster_Greatsword,

        // 2 star
        Iron_Point,
        Old_Mercs_Pal,
        Pocket_Grimoire,
        Seasoned_Hunters_Bow,
        Silver_Sword,

        // 3 star
        Black_Tassel,
        Bloodtainted_Greatsword,
        Cool_Steel,
        Dark_Iron_Sword,
        Debate_Club,
        Emerald_Orb,
        Ferrous_Shadow,
        Fillet_Blade,
        Halberd,
        Harbinger_of_Dawn,
        Magic_Guide,
        Messenger,
        Otherworldly_Story,
        Raven_Bow,
        Recurve_Bow,
        Sharpshoters_Oath,
        Skyride_Greatsword,
        Skyrider_Sword,
        Slingshot,
        Thrilling_Tales_of_Dragon_Slayers,
        Travelers_Handy_Sword,
        Twin_Nephrite,
        White_Iron_Greatsword,
        White_Tassel,

        // 4 star
        The_Catch,

        // 5 star
        A_Thousand_Blazing_Suns,
        A_Thousand_Floating_Dreams,
        Absolution,
        Amos_Bow,
        Aqua_Simulacra,
        Aquila_Favonia,
        Astral_Vultures_Crimson_Plumage,
        Azurelight,
        Beacon_of_the_Reed_Sea,

    }

    public class Weapon
    {
        public WeaponNames Name { get; private set; }
        public WeaponType Type { get; private set; }
        public float BaseATK { get; private set; }
        public string SecondaryAttribute { get; private set; }
        public float SecondaryValue { get; private set; }
        public string Passive { get; private set; }
        public int Refinements { get; private set; }

        public Weapon(WeaponNames name)
        {
            Name = name;

            var data = WeaponDataLoader.Get(name);
            Type = Enum.Parse<WeaponType>(data.Type);
            BaseATK = data.BaseAtk;
            SecondaryAttribute = data.SecondaryAttribute;
            SecondaryValue = data.SecondaryValue;
            Passive = data.Passive;

            Refinements = 1;
        }

        public void LoadWeaponData()
        {
            WeaponDataLoader.Load("Data/weapon_data.json");
        }

        public void SetRefinement(int value)
        {
            Refinements = Math.Clamp(value, 1, 5);
        }
    }

    public class WeaponDataLoader
    {
        public class WeaponJsonData()
        {
            public string Type { get; set; } = "";
            public float BaseAtk { get; set; }
            public string SecondaryAttribute { get; set; } = "";
            public float SecondaryValue { get; set; }
            public string Passive { get; set; } = "";
        }

        private static Dictionary<WeaponNames, WeaponJsonData>? _data;

        public static void Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Weapon data file not found: {jsonPath}");

            string json = File.ReadAllText(jsonPath);

            var tempDict = JsonSerializer.Deserialize<Dictionary<string, WeaponJsonData>>(json)
                            ?? throw new InvalidOperationException("Failed to parse JSON.");

            _data = tempDict.ToDictionary(
                kv => Enum.Parse<WeaponNames>(kv.Key.Replace(" ", "_")),
                kv => kv.Value
            );
        }

        public static WeaponJsonData Get(WeaponNames name)
        {
            if (_data == null)
                throw new InvalidOperationException("Weapon data not loaded. Call WeaponDataLoader.Load() first.");

            if (!_data.TryGetValue(name, out var info))
                throw new ArgumentException($"No weapon data found for {name}");

            return info;
        }

        public static void Set(WeaponNames name, WeaponJsonData data, string jsonPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException($"Weapon data file not found: {jsonPath}");

            string json = File.ReadAllText(jsonPath);

            var tempDict = JsonSerializer.Deserialize<Dictionary<string, WeaponJsonData>>(json)
                ?? new Dictionary<string, WeaponJsonData>();

            string key = name.ToString().Replace("_", " ");

            if (!tempDict.TryAdd(key, data))
                tempDict[key] = data;

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(jsonPath, JsonSerializer.Serialize(tempDict, options));

            _data = tempDict.ToDictionary(
                kv => Enum.Parse<WeaponNames>(kv.Key.Replace(" ", "_")),
                kv => kv.Value
            );
        }
    }
}
