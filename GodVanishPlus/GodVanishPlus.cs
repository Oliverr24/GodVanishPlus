using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;


namespace GodVanishPlus {
    public class GodVanishPlus : RocketPlugin<Configuration> {

        public static GodVanishPlus Instance;

        public static string directory = "Plugins/GodVanishPlus/Staff-Kills.txt";

        protected override void Load() {
            Instance = this;

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;

            if (File.Exists(directory)) {
                Rocket.Core.Logging.Logger.Log("Staff-Kills.txt file has already been created!");
            } else {
                File.CreateText(directory);
                Rocket.Core.Logging.Logger.Log("Staff-Kills.txt has been created!");
            }

            if (Configuration.Instance.Enabled == false) {
                Rocket.Core.Logging.Logger.Log("Plugin has been disabled in the configuration file. Unloaded!");
                base.UnloadPlugin();
            }

            Rocket.Core.Logging.Logger.Log("GodVanishPlus has loaded!");
        }

        protected override void Unload() {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            Rocket.Core.Logging.Logger.Log("GodVanishPlus has unloaded!");
        }

        private void FixedUpdate() {

        }

        int frames = 0;
        private void Update() {
            frames++;

            if (frames > 60) {
                if (Provider.clients.Count > 0) {
                    foreach(SteamPlayer sPlayer in Provider.clients) {
                        UnturnedPlayer player = UnturnedPlayer.FromSteamPlayer(sPlayer);
                        if (!(player.HasPermission("godvanishplus.dequip"))) {
                            return;

                        }else if (player.Features.GodMode || player.Features.VanishMode) {
                            if (player.Player.equipment.isEquipped) {
                                player.Player.equipment.dequip();
                                frames = 0;
                            }
                        }
                    }
                }
            }
        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer) {
            string path = directory;
            List<string> Staff = new List<string>(Configuration.Instance.StaffGroups);


            if (!(player is UnturnedPlayer)) {
                return;
            } else {
                UnturnedPlayer killer = (UnturnedPlayer) UnturnedPlayer.FromCSteamID(murderer);
                foreach (RocketPermissionsGroup pGroup in R.Permissions.GetGroups(killer, true)) {
                    if (Configuration.Instance.StaffGroups.Contains(pGroup.Id)) {
                        if (killer.Features.GodMode) {
                            if (Configuration.Instance.ChatAnnounce == true) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: God Mode!");
                            }
                            File.AppendAllText(path, DateTime.Now.ToString() + "[##GOD ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                            return;
                        } else if (killer.Features.VanishMode) {
                            if (Configuration.Instance.ChatAnnounce == true) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: Vanish Mode!");
                            }
                            File.AppendAllText(path, DateTime.Now.ToString() + "[##VANISH ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                            return;
                        } else if (killer.Features.GodMode && killer.Features.VanishMode) {
                            if (Configuration.Instance.ChatAnnounce == true) {
                                UnturnedChat.Say(player.DisplayName + " was killed by: " + killer.DisplayName + ". They were in: God Mode and Vanish Mode!");
                            }
                            File.AppendAllText(path, DateTime.Now.ToString() + "[##GOD AND VANISH ABUSE##]" + player.DisplayName + " was killed by " + killer.DisplayName + "." + System.Environment.NewLine);
                            return;
                        } else {
                            return;
                        }
                    }

                }
            }

        }

    }
}
