namespace API.DTOs
{
    public class FindDTO
    {
        public List<long> MostEquippedByWinningCharacter { get; set; } = new();
        public List<long> MostBossWinningUser { get; set; } = new();
        

        public void AddMostEquippedByWinningCharacter(long time)
        {
            MostEquippedByWinningCharacter.Add(time);
        }

        public void AddMostBossWinningUser(long time)
        {
            MostBossWinningUser.Add(time);
        }


        public override string ToString()
        {
            return $"FindDTO {{ GetMostEquippedByWinningCharacter = [{string.Join(", ", MostEquippedByWinningCharacter)}], " +
                   $"MostBossWinningUser = [{string.Join(", ", MostBossWinningUser)}] }}";
        }
    }
}
