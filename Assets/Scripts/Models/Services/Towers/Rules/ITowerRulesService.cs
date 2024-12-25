using Models.Services.Towers;

namespace Models.Services
{
    public interface ITowerRulesService
    {
        bool CheckCanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel);
    }
}