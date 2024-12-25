using Models.Services.Towers;
using Models.Services.Towers.Rules;
using System.Collections.Generic;

namespace Models.Services
{
    public class TowerRulesService : ITowerRulesService
    {
        private readonly List<BaseTowerRule> _rules;

        public TowerRulesService()
        {
            _rules = new()
            {
                //new OnlyOneColorTowerRule()
            };
        }

        public bool CheckCanAddBlockInTower(GameBlock gameBlock, TowerModel towerModel)
        {
            return _rules.TrueForAll(rule => rule.CanAddBlockInTower(gameBlock, towerModel));
        }
    }
}
