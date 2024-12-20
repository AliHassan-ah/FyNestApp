﻿using CrudAppProject.Debugging;

namespace CrudAppProject
{
    public class CrudAppProjectConsts
    {
        public const string LocalizationSourceName = "CrudAppProject";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "73d29d1b8c0d48f9b345db4c4ccdb6e9";
    }
}
