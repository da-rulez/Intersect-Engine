using Intersect.Client.Framework.Items;
using Intersect.Framework.Core;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Network.Packets.Server;
using Newtonsoft.Json;


namespace Intersect.Client.Items;


public partial class MapItemInstance : Item, IMapItemInstance
{
    /// <summary>
    /// The Unique Id of this particular MapItemInstance so we can refer to it elsewhere.
    /// </summary>
    public Guid Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    [JsonIgnore] public int TileIndex => Y * Options.Instance.Map.MapWidth + X;

    /// <summary>
    /// The origin X position where the item was spawned from (e.g., NPC position).
    /// Used for scatter animation. Null if no animation needed.
    /// </summary>
    public int? OriginX { get; set; }

    /// <summary>
    /// The origin Y position where the item was spawned from (e.g., NPC position).
    /// Used for scatter animation. Null if no animation needed.
    /// </summary>
    public int? OriginY { get; set; }

    /// <summary>
    /// The time when this item was spawned (for scatter animation).
    /// </summary>
    public long SpawnTime { get; set; }

    /// <summary>
    /// Whether this item has an active scatter animation.
    /// </summary>
    [JsonIgnore]
    public bool HasScatterAnimation => OriginX.HasValue && OriginY.HasValue;

    /// <summary>
    /// Gets the animation progress from 0.0 (start) to 1.0 (complete).
    /// </summary>
    [JsonIgnore]
    public float AnimationProgress
    {
        get
        {
            if (!HasScatterAnimation || SpawnTime == 0)
            {
                return 1f;
            }

            var elapsed = Timing.Global.Milliseconds - SpawnTime;
            var duration = Options.Instance.Loot.ScatterAnimationDuration;
            if (duration <= 0)
            {
                return 1f;
            }

            return Math.Clamp(elapsed / (float)duration, 0f, 1f);
        }
    }

    /// <summary>
    /// Whether the scatter animation is still playing.
    /// </summary>
    [JsonIgnore]
    public bool IsAnimating => HasScatterAnimation && AnimationProgress < 1f;

    public MapItemInstance() : base()
    {
    }

    public MapItemInstance(int tileIndex, Guid uniqueId, Guid itemId, Guid? bagId, int quantity, ItemProperties itemProperties, int? originTileIndex = null) : base()
    {
        Id = uniqueId;
        X = tileIndex % Options.Instance.Map.MapWidth;
        Y = (int)Math.Floor(tileIndex / (float)Options.Instance.Map.MapWidth);
        ItemId = itemId;
        BagId = bagId;
        Quantity = quantity;
        ItemProperties = itemProperties;

        // Set up scatter animation if origin is provided and different from destination
        if (originTileIndex.HasValue && originTileIndex.Value != tileIndex)
        {
            OriginX = originTileIndex.Value % Options.Instance.Map.MapWidth;
            OriginY = (int)Math.Floor(originTileIndex.Value / (float)Options.Instance.Map.MapWidth);
            SpawnTime = Timing.Global.Milliseconds;
        }
    }

}
