using ClassicUO.Custom.Model;
using ClassicUO.Utility.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static ClassicUO.Game.Data.StaticFilters;

namespace ClassicUO.Game.Data.PreferencesJson
{
    public static class PreferenceWallManager
    {
        private const string FILENAME = "wall.json";
        private static readonly string filePath = Path.Combine(DirectoryPath, FILENAME);

        public static void LoadWallFile()
        {
            if (!File.Exists(filePath))
            {
                CreateDefaultFile();
            }

            try
            {
                var content = File.ReadAllText(filePath);
                WallTiles = JsonSerializer.Deserialize<List<CustomItens>>(content);
                Log.Trace($"File {filePath} load with sucess!");
            }
            catch (Exception ex)
            {
                Log.Warn($"File {filePath} is Wrong. {ex.Message}");
            }
        }

        public static void CreateDefaultFile()
        {
            List<CustomItens> replaceWall = [];
            List<ushort> wallTiles = [0x01FF, 0x0200, 0x0201, 0x0202, 0x0203, 0x0204, 0x0205, 0x0206];

            replaceWall.Add(new CustomItens()
            {
                Description = "Default",
                ReplaceToGraphic = Constants.WALL_REPLACE_GRAPHIC,
                ToReplaceGraphicArray = wallTiles
            });

            var jsonString = JsonSerializer.Serialize(replaceWall);
            File.WriteAllText(filePath, jsonString);
        }

        public static void UpdateFile()
        {
            List<CustomItens> replaceWall = [];
            List<ushort> wallTiles = [0x01FF, 0x0200, 0x0201, 0x0202, 0x0203, 0x0204, 0x0205, 0x0206];

            replaceWall.Add(new CustomItens()
            {
                Description = "Default",
                ReplaceToGraphic = Constants.WALL_REPLACE_GRAPHIC,
                ToReplaceGraphicArray = wallTiles
            });

            var jsonString = JsonSerializer.Serialize(replaceWall);
            File.WriteAllText(filePath, jsonString);
        }

        public static void RemoveGraphic(ushort graphic)
        {
            var content = File.ReadAllText(filePath);
            try
            {
                var customItens = JsonSerializer.Deserialize<List<CustomItens>>(content);               
                       
                foreach (var itemList in customItens)
                {
                    itemList.ToReplaceGraphicArray.Remove(graphic);
                }
                content = JsonSerializer.Serialize(customItens);
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                Log.Warn($"File {filePath} is Wrong. {ex.Message}");
            }
        }
    }

}
