using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Common
{
    public class ProjectConstants
    {
        public class Common
        {
            public static string DictionariesPath
            {
                get
                {
#if UNITY_EDITOR
                    return "Assets/Saves";
#elif UNITY_STANDALONE
                var appDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                var gamePath = System.IO.Path.Combine(appDataPath, UnityEngine.Application.companyName, UnityEngine.Application.productName);
                if (!System.IO.Directory.Exists(gamePath))
                    System.IO.Directory.CreateDirectory(gamePath);
                return gamePath;
#endif
                }
            }
        }

        public class Serialization
        {
            public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented,

                Error = delegate (object sender, ErrorEventArgs args)
                {
                    UnityEngine.Debug.LogError(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                }
            };
        }

        public class Localization
        {
            public const string UI_TABLE_NAME = "Ui";

            public class Messages
            {
                public const string CREATE_BLOCK_ON_TOWER_KEY = "CreateBlockOnTower";
                public const string THROW_BLOCK_IN_GARBAGE_KEY = "ThrowBlockInGarbage";
                public const string DESTROY_BLOCK_KEY = "DestroyBlock";
                public const string TOWER_HEIGHT_LIMIT_KEY = "TowerHeightLimit";
                public const string WRONG_BLOCK_DROP_RULE_KEY = "WrongBlockDropRule";
                public const string WRONG_BLOCK_DROP_POSITION_KEY = "WrongBlockDropPosition";
                public const string GET_BLOCK_FROM_TOWER_KEY = "GetBlockFromTower";
                public const string RETURN_BLOCK_IN_TOWER_KEY = "ReturnBlockInTower";
            }
        }
    }
}
