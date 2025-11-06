using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genshin.Elements;

namespace Genshin.Attributes
{
    public enum EnergyParticle
    {
        ElementalParticle,
        ElementalOrb,
        ClearParticle,
        ClearOrb,
    }

    public class ElementalParticle()
    {
        public Element Element { get; private set; }
        public EnergyParticle Particle { get; private set; }

        public Dictionary<EnergyParticle, float> ParticleValues = new()
        {
            { EnergyParticle.ElementalParticle, 1.0f },
            { EnergyParticle.ElementalOrb, 3.0f },
            { EnergyParticle.ClearParticle, 2.0f },
            { EnergyParticle.ClearOrb, 6.0f }
        };

        public ElementalParticle(Element element, EnergyParticle particle)
        {
            Element = element;
            Particle = particle;
        }

        private bool SameElement(Element other)
        {
            return other == Element;
        }

        public float CatchParticle(Character character)
        {
            float particleValue = ParticleValues.TryGetValue(Particle, out var value) ? value : 1;
            float elementMult = SameElement(character.Element) ? 3 : 1;
            return particleValue * elementMult; 
        }
    }
}
