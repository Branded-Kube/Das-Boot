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
        void Open();

        void Close();
    }
}
