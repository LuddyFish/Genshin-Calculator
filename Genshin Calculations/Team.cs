using Genshin.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genshin
{
    public enum ElementalResonance
    {
        None,
        Pyro,
        Hydro,
        Electro,
        Cryo,
        Anemo,
        Geo,
        Dendro,
        Mix
    }

    public class Team
    {
        public List<Character> Composition {  get; set; }
        public List<ElementalResonance> Resonance { get; set; }

        public Team()
        {
            Composition = new List<Character>();
            Resonance = new List<ElementalResonance>();
        }

        public void AddCharacter(Character character)
        {
            Composition.Add(character);
        }

        public void RemoveCharacter(Character character)
        {
            if (Composition.Remove(character))
                Console.WriteLine($"{character.Name} was removed from the team.");
            else
                Console.WriteLine($"{character.Name} is not a part of the team.");
        }

        private void AddResonance(ElementalResonance element)
        {
            if (Resonance.Contains(element)) Resonance.Add(element);
        }

        public void SetElementalResonance()
        {
            while (Resonance.Count > 0) Resonance.Clear();
            if (Composition.Count < 4)
            {
                Resonance.Add(ElementalResonance.None);
                return;
            }

            foreach (Character character in Composition)
            {
                switch (character.Element)
                {
                    case Element.Pyro: AddResonance(ElementalResonance.Pyro); break;
                    case Element.Hydro: AddResonance(ElementalResonance.Hydro); break;
                    case Element.Electro: AddResonance(ElementalResonance.Electro); break;
                    case Element.Cryo: AddResonance(ElementalResonance.Cryo); break;
                    case Element.Anemo: AddResonance(ElementalResonance.Anemo); break;
                    case Element.Geo: AddResonance(ElementalResonance.Geo); break;
                    case Element.Dendro: AddResonance(ElementalResonance.Dendro); break;
                }
            }
            if (Resonance.Count == 0) AddResonance(ElementalResonance.Mix);
        }
    }
}
