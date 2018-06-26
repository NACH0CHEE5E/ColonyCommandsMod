﻿using Pipliz;
using Pipliz.Chatting;
using ChatCommands;

namespace ScarabolMods
{
  public class AreaShowCommand : IChatCommand
  {
    public bool IsCommand (string chat)
    {
      return chat.Equals ("/areashow");
    }

    public bool TryDoCommand (Players.Player causedBy, string chattext)
    {
      Vector3Int playerPos = new Vector3Int(causedBy.Position);
      CustomProtectionArea closestArea = null;
      int shortestDistance = -1;
      foreach (CustomProtectionArea area in AntiGrief.CustomAreas) {
        if (area.Contains(playerPos)) {
          Chat.Send(causedBy, $"You are inside a custom area: from {area.StartX}, {area.StartZ} to {area.EndX}, {area.EndZ}");
          return true;
        }
        int distance = area.DistanceToCenter(playerPos);
        if (shortestDistance == -1 || distance < shortestDistance) {
          shortestDistance = distance;
          closestArea = area;
        }
      }

      if (closestArea != null) {
        Chat.Send(causedBy, $"The closest area is at: {closestArea.StartX}, {closestArea.StartZ} to {closestArea.EndX}, {closestArea.EndZ}. {shortestDistance} blocks away");
      } else {
        Chat.Send(causedBy, "No areas found - try /bannername?");
      }

      return true;
    }
  }
}
