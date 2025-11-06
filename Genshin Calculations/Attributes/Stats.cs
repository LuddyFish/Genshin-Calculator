using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genshin.Attributes
{
    public class Stats
    {
        public float BaseHP { get; set; }
        public float BaseATK { get; set; }
        public float BaseDEF { get; set; }

        public int HP;
        public int ATK;
        public int DEF;
        public int Elemental_Mastery;
        public float Energy_Recharge;
        public float CRIT_DMG;
        public float CRIT_Rate;
        public float Physical_DMG_Bonus;
        public float Pyro_DMG_Bonus;
        public float Hydro_DMG_Bonus;
        public float Electro_DMG_Bonus;
        public float Cryo_DMG_Bonus;
        public float Anemo_DMG_Bonus;
        public float Geo_DMG_Bonus;
        public float Dendro_DMG_Bonus;
        public float Healing_Bonus;

        public Stats(float BaseHP, float BaseATK, float BaseDEF)
        {
            this.BaseHP = BaseHP;
            this.BaseATK = BaseATK;
            this.BaseDEF = BaseDEF;
        }

        public int AddHP(float percentage)
        {
            return (int)MathF.Round(BaseHP * percentage);
        }

        public int AddATK(float percentage)
        {
            return (int)MathF.Round(BaseATK * percentage);
        }

        public int AddDEF(float percentage)
        {
            return (int)MathF.Round(BaseDEF * percentage);
        }

        public float GetCritValue()
        {
            return CRIT_DMG + CRIT_Rate * 2;
        }

        public float GetCritMV()
        {
            return CRIT_DMG * (CRIT_Rate * 0.01f);
        }
    }

    public class Energy
    {
        
    }
}
