
namespace DataBros
{
    public interface IRepository
    {

        Water FindWater(string name);
        void AddWater(string name, int size, bool type);

        Fish FindFish(string name);
        void AddFish(string name,int weight, int price, int FKID, int strenght);
        Player FindPlayer(string name);

        void AddPlayer(string name, int money, string password);

        void DelPlayers();
        void UpdatePlayers(string name, int price);

        void AddBait(string name, int price, int biteTime, bool alive);
        Bait FindBait(string name);
        void Open();

        void Close();
    }
}
