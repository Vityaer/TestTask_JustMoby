namespace Models.Services.Towers.Rules
{
    public interface ITowerRule
    {
        bool CanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel);
    }
}