using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genshin
{
    public enum Set
    {
        [Description("None")] None,

        // ===== Version 1.0 (Mondstadt/Liyue) =====
        [Description("Adv")] Adventurer,
        [Description("LD")] Lucky_Dog,
        [Description("TD")] Traveling_Doctor,
        [Description("RoS")] Resolution_of_Sojourner,
        [Description("TM")] Tiny_Miracle,
        [Description("Bers")] Berserker,
        [Description("Inst")] Instructor,
        [Description("TE")] The_Exile,
        [Description("DW")] Defenders_Will,
        [Description("BH")] Brave_Heart,
        [Description("MA")] Martial_Artist,
        [Description("Gamb")] Gambler,
        [Description("Schl")] Scholar,
        [Description("PrayW")] Prayers_for_Wisdom,
        [Description("PrayD")] Prayers_for_Destiny,
        [Description("PrayI")] Prayers_for_Illumination,
        [Description("PrayS")] Prayers_to_Springtime,
        [Description("GF")] Gladiators_Finale,
        [Description("WT")] Wanderers_Troupe,
        [Description("NO")] Noblesse_Oblige,
        [Description("BC")] Bloodstained_Chivalry,
        [Description("MB")] Maiden_Beloved,
        [Description("VV")] Viridescent_Venerer,
        [Description("LW")] Lavawalker,
        [Description("TS")] Thundersoother,
        [Description("TF")] Thundering_Fury,
        [Description("CWoF")] Crimson_Witch_of_Flames,
        [Description("AP")] Archaic_Petra,
        [Description("RB")] Retracing_Bolide,

        // ===== Version 1.2 =====
        [Description("BS")] Blizzard_Strayer,
        [Description("HoD")] Heart_of_Depth,

        // ===== Version 1.5 =====
        [Description("TotM")] Tenacity_of_the_Millelith,
        [Description("PF")] Pale_Flame,

        // ===== Version 2.0 (Inazuma) =====
        [Description("EoSF")] Emblem_of_Severed_Fate,
        [Description("SR")] Shimenawas_Reminiscence,

        // ===== Version 2.3 =====
        [Description("HoOD")] Husk_of_Opulent_Dreams,
        [Description("OHC")] Ocean_Hued_Clam,

        // ===== Version 2.6 =====
        [Description("VH")] Vermillion_Hereafter,
        [Description("EoaO")] Echoes_of_an_Offering,

        // ===== Version 3.0 (Sumeru) =====
        [Description("DM")] Deepwood_Memories,
        [Description("GD")] Gilded_Dreams,

        // ===== Version 3.3 =====
        [Description("DPC")] Desert_Pavilion_Chronicle,
        [Description("FoPL")] Flower_of_Paradise_Lost,

        // ===== Version 3.6 =====
        [Description("VG")] Vourukashas_Glow,
        [Description("ND")] Nymphs_Dream,

        // ===== Version 4.0 (Fontaine) =====
        [Description("GT")] Golden_Troupe,
        [Description("MH")] Marechaussee_Hunter,

        // ===== Version 4.3 =====
        [Description("SoDP")] Song_of_Days_Past,
        [Description("NW")] Nighttime_Whispers_in_the_Echoing_Woods,

        // ===== Version 4.6 =====
        [Description("FHW")] Fragment_of_Harmonic_Whimsy,
        [Description("UR")] Unfinished_Reverie,

        // ===== Version 5.0 (Natlan) =====
        [Description("Scroll")] Scroll_of_the_Hero_of_Cinder_City,
        [Description("OC")] Obsidian_Codex,

        // ===== Version 5.5 =====
        [Description("FDG")] Finale_of_the_Deep_Galleries,
        [Description("LNO")] Long_Nights_Oath,

        // ===== Version Luna I (Nod-Krai) =====
        [Description("NotSU")] Night_of_the_Skys_Unveiling,
        [Description("SMS")] Silken_Moons_Serenade,
    }

    public static class EnumExtensions
    {
        public static string GetAbbreviation(this Set value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }
    }

    public class Artifact
    {
        public Set Set;

        public Artifact(Set name)
        {
            Set = name;
        }
    }
}
