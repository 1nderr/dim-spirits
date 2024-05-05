using Godot;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class ItemData
{
	public string name { get; set; }
	public string scenePath { get; set; }
	public string spritePath { get; set; }
}

public partial class ItemStore : Node
{
	private Dictionary<int, ItemData> items = new();

	public override void _Ready()
	{
		string json = File.ReadAllText("./data/item_table.json");
		items = JsonSerializer.Deserialize<Dictionary<int, ItemData>>(json);
	}

	public Texture2D GetItemSprite(int id)
	{
		return (Texture2D)GD.Load(items[id].spritePath);
	}

	public ItemData GetItemData(int id)
	{
		return items[id];
	}
}
