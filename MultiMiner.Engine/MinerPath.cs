﻿using MultiMiner.Xgminer;
using System;
using System.IO;

namespace MultiMiner.Engine
{
    public static class MinerPath
    {
        private const string CgminerName = "cgminer";
        private const string BfgminerName = "bfgminer";

        public static string GetPathToInstalledMiner(MinerBackend minerBackend)
        {
            string executablePath = string.Empty;
            string minerName = GetMinerName(minerBackend);

            switch (OSVersionPlatform.GetGenericPlatform())
            {
                case PlatformID.Unix:
                    //try local path first
                    executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Miners/{0}/bin/{0}", minerName));
                                        
                    if (!File.Exists(executablePath))
                        //try global path (Homebrew)
                        executablePath = string.Format(@"/usr/local/bin/{0}", minerName);

                    break;
                default:
                    executablePath = string.Format(@"Miners\{0}\{0}.exe", minerName);
                    break;
            }

            return executablePath;
        }

        public static string GetMinerName(MinerBackend minerBackend)
        {
            string minerName = CgminerName;

            if (minerBackend == MinerBackend.Bfgminer)
                minerName = BfgminerName;

            return minerName;
        }
    }
}
