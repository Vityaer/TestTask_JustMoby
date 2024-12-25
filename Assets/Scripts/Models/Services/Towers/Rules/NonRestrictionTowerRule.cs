namespace Models.Services.Towers.Rules
{
    public class NonRestrictionTowerRule : BaseTowerRule
    {
        public override bool CanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel)
        {
            return true;
        }
    }
}
