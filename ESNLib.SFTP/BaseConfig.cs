using System;
using WinSCP;

namespace ESNLib.SFTP
{
    public class BaseConfig
    {
        /// <summary>
        /// Hostname
        /// </summary>
        public string Hostname { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Passphrase for private key
        /// </summary>
        public string Passphrase { get; set; }

        /// <summary>
        /// Private Key Path
        /// </summary>
        public string PrivateKeyPath { get; set; }

        /// <summary>
        /// Host Key fingerprint Path
        /// </summary>
        public string HostKeyPath { get; set; }

        /// <summary>
        /// Port to be used
        /// </summary>
        public int Port { get; set; } = 22;

        /// <summary>
        /// Ftp mode to be used
        /// </summary>
        public FtpMode FtpMode { get; set; } = FtpMode.Passive;

        /// <summary>
        /// List of configs
        /// </summary>
        public Config[] Configs { get; set; }

        /// <summary>
        /// Check wheter all parameters are set
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Hostname) ||
                string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Passphrase) ||
                string.IsNullOrEmpty(PrivateKeyPath) ||
                string.IsNullOrEmpty(HostKeyPath))
                return false;
            return true;
        }

        public class Config : IComparable<Config>
        {
            /// <summary>
            /// Is this config enabled
            /// </summary>
            public bool Enabled { get; set; } = true;
            /// <summary>
            /// Priority to run this config, the higher this value, the higher the priority
            /// </summary>
            public int RunPriority { get; set; }
            /// <summary>
            /// Determine the synchronization mode. Default : Remote. Local = 1, Remote = 2, Both = 3
            /// </summary>
            public SynchronizationMode SyncMode { get; set; } = SynchronizationMode.Remote;
            /// <summary>
            /// Determine the synchronization criteria. Default : Time
            /// </summary>
            public SynchronizationCriteria SyncCriteria { get; set; } = SynchronizationCriteria.Time;
            /// <summary>
            /// Determine the transfer mode. Default : Binary
            /// </summary>
            public TransferMode TransferMode { get; set; } = TransferMode.Binary;
            /// <summary>
            /// Local path
            /// </summary>
            public string LocalPath { get; set; }
            /// <summary>
            /// FileMask for the transfer
            /// </summary>
            public string FileMask { get; set; }
            /// <summary>
            /// Remote path
            /// </summary>
            public string RemotePath { get; set; }
            /// <summary>
            /// Remove files. True to remove, False to keep
            /// </summary>
            public bool RemoveFiles { get; set; } = false;
            /// <summary>
            /// Mirror mode
            /// </summary>
            public bool Mirror { get; set; } = false;
            /// <summary>
            /// Is sync mode enabled or are we just uploading files wihout checking. True = sync mode, False = upload mode
            /// </summary>
            public bool SyncModeEnabled { get; set; } = true;

            /// <summary>
            /// Compare according to run priority (0 is lowest priority, negatives excluded)
            /// </summary>
            public int CompareTo(Config other)
            {
                // Negatives numbers are last priority
                if (this.RunPriority < 0)
                {
                    // If also last priority, set equal
                    if (other.RunPriority < 0)
                        return 0;
                    return -1;
                }
                // Priority close to 0 comes first
                if (this.RunPriority < other.RunPriority)
                    return 1;
                if (this.RunPriority > other.RunPriority)
                    return -1;
                return 0;
            }

            /// <summary>
            /// Indicate if LocalPath and RemotePath are set (don't check if they exists)
            /// </summary>
            /// <returns></returns>
            public bool IsValid()
            {
                if (LocalPath == null || RemotePath == null)
                    return false;
                if (LocalPath == string.Empty || RemotePath == string.Empty)
                    return false;
                return true;
            }

            public override string ToString()
            {
                return $"Config@{GetHashCode()} []";
            }
        }
    }
}
