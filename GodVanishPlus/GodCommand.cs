using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace GodVanishPlus {
    public class GodCommand : IRocketCommand {

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "god";

        public string Help => "Enable God Mode!";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() {
            "godvanishplus.god"
        };

        public void Execute(IRocketPlayer caller, string[] command) {


            if (!caller.HasPermission("godvanishplus.god")) {
                UnturnedChat.Say(caller, "You do not have access to use this command!", Color.red);
                return;
            }

            if (caller is ConsolePlayer && command.Length < 1) {
                Rocket.Core.Logging.Logger.Log("This command cannot be called from console. Try /god <player>");
                return;
            }
            else if (caller is UnturnedPlayer && command.Length < 1) {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (player.Features.GodMode) {
                    UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.godOffMsg, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godColorOff, Color.red));
                    Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned off God Mode!");
                    player.Features.GodMode = false;
                    return;
                }
                else {
                    UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.godOnMsg, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godColorOn, Color.cyan));
                    Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned on God Mode!");
                    player.Features.GodMode = true;
                    return;
                }
            }
            else if (caller is ConsolePlayer && command.Length >= 1 || caller is UnturnedPlayer) {
                if (caller.HasPermission("nebulafalls.god.others")) {
                    UnturnedPlayer godPlayer = (UnturnedPlayer)UnturnedPlayer.FromName(command[0]);

                    if (godPlayer == null) {
                        UnturnedChat.Say(caller, "This player could not be found!", Color.red);
                        return;
                    }
                    else {
                        if (godPlayer.Features.GodMode) {
                            godPlayer.Features.GodMode = false;
                            UnturnedChat.Say(godPlayer, GodVanishPlus.Instance.Configuration.Instance.godOthersOff + caller.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godOthersColor, Color.yellow));
                            UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.takenOthersGod + godPlayer.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godOthersColor, Color.yellow));
                            Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned off God Mode for: " + godPlayer.DisplayName);
                            return;
                        }
                        else {
                            godPlayer.Features.GodMode = true;
                            UnturnedChat.Say(godPlayer, GodVanishPlus.Instance.Configuration.Instance.godOthersOn + caller.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godOthersColor, Color.yellow));
                            UnturnedChat.Say(caller, GodVanishPlus.Instance.Configuration.Instance.givenOthersGod + godPlayer.DisplayName, UnturnedChat.GetColorFromName(GodVanishPlus.Instance.Configuration.Instance.godOthersColor, Color.yellow));
                            Rocket.Core.Logging.Logger.Log(caller.DisplayName + " has turned on God Mode for: " + godPlayer.DisplayName);
                            return;
                        }
                    }
                }
                else {
                    UnturnedChat.Say(caller, "You do not have permission to give other players god!", Color.red);
                    return;
                }
            }
        }
    }
}
