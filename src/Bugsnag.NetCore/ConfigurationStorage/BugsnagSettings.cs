using System;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Bugsnag.NetCore.ConfigurationStorage
{
    public class BugsnagSettings
    {
        public BugsnagSettings()
        {
            ApiKey = "12345678901234567890123456789012";
            Endpoint = "https://notify.bugsnag.com";
            AutoDetectInProject = true;
            AutoNotify = true;
            IgnoreClasses = new string[] { };
            MetadataFilters = new string[] { };
            ProjectNamespaces = new string[] { };
            FilePrefixes = new string[] { };
            StoreOfflineErrors = false;
        }

        public BugsnagSettings(string apiKey)
        {
            ApiKey = apiKey;
            Endpoint = "https://notify.bugsnag.com";
            AutoDetectInProject = true;
            AutoNotify = true;
            IgnoreClasses = new string[] { };
            MetadataFilters = new string[] { };
            ProjectNamespaces = new string[] { };
            FilePrefixes = new string[] { };
            StoreOfflineErrors = false;
        }

        public string ApiKey { get; set; }

        public string AppVersion { get; set; }

        public string ReleaseStage { get; set; }

        public string Endpoint { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string Context { get; set; }

        public bool AutoDetectInProject { get; set; }

        public bool AutoNotify { get; set; }

        private string NotifyReleaseStagesString { get; set; }
        private string[] _notifyReleaseStages;
        public string[] NotifyReleaseStages
        {
            get
            {
                if (this._notifyReleaseStages == null)
                {
                    if (String.IsNullOrEmpty(this.NotifyReleaseStagesString))
                    {
                        return null;
                    }
                    else
                    {
                        this._notifyReleaseStages = this.NotifyReleaseStagesString.Split(',');
                        return this._notifyReleaseStages;
                    }
                }
                else
                {
                    return this._notifyReleaseStages;
                }
            }
            set { this._notifyReleaseStages = value; }
        }

        private string FilePrefixesString { get; set; }
        private string[] _filePrefixes;
        public string[] FilePrefixes
        {
            get
            {
                if (this._filePrefixes == null)
                {
                    this._filePrefixes = String.IsNullOrEmpty(this.FilePrefixesString) ? new string[] { } : this.FilePrefixesString.Split(',');
                    return this._filePrefixes;
                }
                else
                {
                    return this._filePrefixes;
                }
            }
            set { this._filePrefixes = value; }
        }

        private string ProjectNamespacesString { get; set; }
        private string[] _projectNamespaces;
        public string[] ProjectNamespaces
        {
            get
            {
                if (this._projectNamespaces == null)
                {
                    this._projectNamespaces = String.IsNullOrEmpty(this.ProjectNamespacesString) ? new string[] { } : this.ProjectNamespacesString.Split(',');
                    return this._projectNamespaces;
                }
                else
                {
                    return this._projectNamespaces;
                }
            }
            set { this._projectNamespaces = value; }
        }

        private string IgnoreClassesString { get; set; }
        private string[] _ignoreClasses;
        public string[] IgnoreClasses
        {
            get
            {
                if (this._ignoreClasses == null)
                {
                    this._ignoreClasses = String.IsNullOrEmpty(this.IgnoreClassesString) ? new string[] { } : this.IgnoreClassesString.Split(',');
                    return this._ignoreClasses;
                }
                else
                {
                    return this._ignoreClasses;
                }
            }
            set { this._ignoreClasses = value; }
        }

        private string MetadataFiltersString { get; set; }
        private string[] _metadataFilters;
        public string[] MetadataFilters
        {
            get
            {
                if (this._metadataFilters == null)
                {
                    this._metadataFilters = String.IsNullOrEmpty(this.MetadataFiltersString) ? new string[] { } : this.MetadataFiltersString.Split(',');
                    return this._metadataFilters;
                }
                else
                {
                    return this._metadataFilters;
                }
            }
            set { this._metadataFilters = value; }
        }

        public bool StoreOfflineErrors { get; set; }

    }
}
