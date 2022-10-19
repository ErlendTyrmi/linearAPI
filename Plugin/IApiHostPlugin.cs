using Microsoft.Extensions.Logging;

namespace Plugin
{
    public interface IApiHostPlugin
    {
        /// <summary>
        /// Gets or sets the common settings for the plugin (automatically instantized by the host).
        /// </summary>
        PluginSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the connection string to the Booster database.
        /// </summary>
        string BoosterConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the logger mechanisms
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// Gets the domain that the plugin implements.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the version of the implementation.
        /// </summary>
        string Version { get; }
    }
}