using System.Diagnostics.CodeAnalysis;
using CustomPlayerEffects;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features;
using LabApi.Features.Wrappers;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace TrueSinkholes;

public sealed class Config
{
    public float TeleportDistanceMult { get; set; } = 0.6f;
    public float SlowDistanceMult { get; set; } = 1f;
}

public sealed class TrueSinkholes : Plugin
{
    public override string Name => "TrueSinkholes";
    public override string Description => "Improves base-game sinkhole effects.";
    public override string Author => "TechConcordance";
    public override Version Version => new(1, 0);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    
    [field: AllowNull] private Config Config => field ??= this.TryLoadConfig<Config>("config.yml", out var config) ? config : new Config();

    public override void Enable()
    {
        LabApi.Events.Handlers.ServerEvents.MapGenerated += MapGenerated;
        LabApi.Events.Handlers.PlayerEvents.StayingInHazard += HazardTick;
    }
    
    public override void Disable()
    {
        LabApi.Events.Handlers.ServerEvents.MapGenerated -= MapGenerated;
        LabApi.Events.Handlers.PlayerEvents.StayingInHazard -= HazardTick;
        SinkholeDistanceModification(1 / Config.SlowDistanceMult);
    }

    private void MapGenerated(MapGeneratedEventArgs ev) => SinkholeDistanceModification(Config.SlowDistanceMult);
    
    private static void SinkholeDistanceModification(float multiplier)
    {
        foreach (var hazard in SinkholeHazard.List.Where(hazard => hazard.Base))
            hazard.MaxDistance *= multiplier;
    }

    private void HazardTick(PlayersStayingInHazardEventArgs ev)
    {
        if (ev.Hazard is not SinkholeHazard { SourcePosition: var center, MaxDistance: var maxDistance })
            return;
        var teleportDistance = maxDistance * Config.TeleportDistanceMult;
        var sqrTeleportDistance = teleportDistance * teleportDistance;
        foreach (var player in from player in ev.AffectedPlayers where !player.HasEffect<PocketCorroding>() && (center - player.Position).SqrMagnitudeIgnoreY() <= sqrTeleportDistance select player)
            player.EnableEffect<PocketCorroding>();
    }
}