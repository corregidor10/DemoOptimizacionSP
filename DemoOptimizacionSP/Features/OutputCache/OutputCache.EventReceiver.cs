using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;

namespace DemoOptimizacionSP.Features.OutputCache
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("c3aa7ea4-abce-4bdc-894d-8ac04dcc85ab")]
    public class OutputCacheEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = (SPSite) properties.Feature.Parent;

            using (SPWeb web= site.RootWeb)
            {
                const int DISABLED_CACHE_PROFILE = 1;

                SPList cacheProfiles= web.Lists["Cache Profiles"];
                SPListItem newCacheProfile = cacheProfiles.Items.Add();
                newCacheProfile["Title"] = "Contoso Anonymous";
                newCacheProfile.Update();

                SiteCacheSettingsWriter cacheSettings= new SiteCacheSettingsWriter(site);

                cacheSettings.EnableCache = true;
                cacheSettings.EnableDebuggingOutput = true;
                cacheSettings.AllowPublishingWebPageOverrides = true;
                cacheSettings.AllowLayoutPageOverrides = true;
                cacheSettings.SetAuthenticatedPageCacheProfileId(site, newCacheProfile.ID);
                cacheSettings.SetAuthenticatedPageCacheProfileId(site, DISABLED_CACHE_PROFILE);
                cacheSettings.SetFarmBlobCacheFlushFlag();
                cacheSettings.Update();
            }

        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
