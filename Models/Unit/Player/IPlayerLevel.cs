namespace Models.Unit.Player
{
    interface IPlayerLevel
    {
        int CurrLevel { get; set; }

        int CurrXP { get; set; }

        int XPForNextLevel { get; set; }

        void LevelUP();
    }
}
