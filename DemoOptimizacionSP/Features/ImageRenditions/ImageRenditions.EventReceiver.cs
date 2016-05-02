using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;


namespace DemoOptimizacionSP.Features.ImageRenditions
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("27d233d1-752d-479f-b57b-cd2b92efdd6d")]
    public class ImageRenditionsEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPSite site = (SPSite) properties.Feature.Parent;

            using (SPWeb web = site.RootWeb)
            {
                ImageRenditionCollection imageRenditions = SiteImageRenditions.GetRenditions(site);

                ImageRendition mastheadRendition =
                    imageRenditions.SingleOrDefault<ImageRendition>(ir => ir.Name == "Masthead");

                if (mastheadRendition == null)
                {
                    mastheadRendition=new ImageRendition();
                    mastheadRendition.Name = "Masthead";
                    mastheadRendition.Width = 966;
                    mastheadRendition.Height = 300;

                    imageRenditions.Add(mastheadRendition);
                    imageRenditions.Update();
                }

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
