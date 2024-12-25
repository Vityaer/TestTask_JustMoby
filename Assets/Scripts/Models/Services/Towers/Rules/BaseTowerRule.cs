namespace Models.Services.Towers.Rules
{
    public abstract class BaseTowerRule : ITowerRule
    {
        public abstract bool CanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel);
    }
}
