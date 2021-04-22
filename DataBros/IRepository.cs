using System;
using System.Collections.Generic;
using System.Text;

namespace DataBros
{
    public interface IRepository
    {
        void AddCharacter(string name, int experience);
        Character FindCharacter(string name);
        List<Character> GetAllCharacters();

        Water FindWater(string name);

        void AddWater(string name, int size, bool type);


        void AddBait(string name, int Cost);
        Bait FindBait(string name);
        List<Bait> GetAllBait();
        void Open();

        void Close();
    }
}
