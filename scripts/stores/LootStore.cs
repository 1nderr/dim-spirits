using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class LootData
{
	public string scenePath { get; set; }
}

public partial class LootStore : Node
{
	private Dictionary<int, LootData> loot = new();

	public override void _Ready()
	{
		string json = File.ReadAllText("./data/loot_table.json");
		loot = JsonSerializer.Deserialize<Dictionary<int, LootData>>(json);
	}

	public Loot GetLoot()
	{
		var index = (int)GD.Randi() % loot.Count;
		PackedScene lootScene = (PackedScene)GD.Load(loot[index + 1].scenePath);
		return lootScene.Instantiate<Loot>();
	}
}
