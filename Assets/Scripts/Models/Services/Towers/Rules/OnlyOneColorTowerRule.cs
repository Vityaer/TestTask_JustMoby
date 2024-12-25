namespace Models.Services.Towers.Rules
{
    public class OnlyOneColorTowerRule : BaseTowerRule
    {
        public override bool CanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel)
        {
            var result = true;
            if (towerModel.Blocks.Count > 0)
            {
                foreach (var block in towerModel.Blocks)
                {
                    if (!block.GameBlock.Id.Equals(gameBlock.Id))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
