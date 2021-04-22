using System;
using System.Collections.Generic;
using System.Text;

namespace DataBros
{
    public interface IAdventurerRepository
    {
        void AddCharacter(string name, int experience);
        Character FindCharacter(string name);
        List<Character> GetAllCharacters();


        void AddBait(string name, int Cost);
        Bait FindBait(string name);
        List<Bait> GetAllBait();
        void Open();

        void Close();
    }
}
