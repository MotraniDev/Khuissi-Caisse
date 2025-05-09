using System;

namespace Khouissi_Caisse.Models;

/// <summary>
/// Represents application-wide settings for the subscription fund system
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Unique identifier for the settings record
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Default monthly subscription amount
    /// </summary>
    public decimal DefaultSubscriptionAmount { get; set; }

    /// <summary>
    /// Organization name (in Arabic)
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// Currency symbol to use in reports and UI
    /// </summary>
    public string CurrencySymbol { get; set; } = "دج";

    /// <summary>
    /// Path for database backups
    /// </summary>
    public string BackupPath { get; set; } = string.Empty;

    /// <summary>
    /// Automatic backup frequency in days (0 = no automatic backup)
    /// </summary>
    public int AutoBackupFrequencyDays { get; set; }

    /// <summary>
    /// Date and time of the last automatic backup
    /// </summary>
    public DateTime? LastBackupDateTime { get; set; }

    /// <summary>
    /// Whether to create backup on Google Drive
    /// </summary>
    public bool EnableGoogleDriveBackup { get; set; }

    /// <summary>
    /// Google Drive folder ID for backups
    /// </summary>
    public string GoogleDriveFolderId { get; set; } = string.Empty;

    /// <summary>
    /// Whether to create backup on USB drive when connected
    /// </summary>
    public bool EnableUsbBackup { get; set; }

    /// <summary>
    /// Whether to check for application updates on startup
    /// </summary>
    public bool CheckForUpdatesOnStartup { get; set; } = true;

    /// <summary>
    /// URL for checking application updates
    /// </summary>
    public string UpdateCheckUrl { get; set; } = string.Empty;

    /// <summary>
    /// Maximum number of backup files to keep
    /// </summary>
    public int MaxBackupFiles { get; set; } = 5;

    /// <summary>
    /// Last modified date of settings
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.Now;

    /// <summary>
    /// ID of the user who last modified the settings
    /// </summary>
    public int LastModifiedByUserId { get; set; }

    /// <summary>
    /// Navigation property to the user who last modified the settings
    /// </summary>
    public User? LastModifiedByUser { get; set; }
}
