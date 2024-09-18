using System.Collections.Generic;

namespace StellarisShips.System
{
    public class SectionForSave
    {
        public string InternalName = "";

        public List<string> WeaponSlot = new();

        public List<string> UtilitySlot = new();

        public SectionForSave Copy()
        {
            SectionForSave other = new();
            other.InternalName = InternalName;
            foreach (string slot in WeaponSlot)
            {
                other.WeaponSlot.Add(slot);
            }
            foreach (string slot in UtilitySlot)
            {
                other.UtilitySlot.Add(slot);
            }
            return other;
        }
    }
}
