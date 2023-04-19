using ApptiqueClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApptiqueClient
{
    public interface IPackageService
    {
        List<PackageModel> GetAllPackages();
    }
}
