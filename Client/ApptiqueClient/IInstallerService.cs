using ApptiqueClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApptiqueClient
{
    internal interface IInstallerService
    {
        Task<bool> InstallAndDownloadPackage(RevisionModel revision, string packagename);
    }
}
