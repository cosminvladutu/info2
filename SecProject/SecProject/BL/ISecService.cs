using SecProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecProject.BL
{
    public interface ISecService
    {
        List<NewsResult> GetNews();

        Dictionary<string, string> ReadConfigs();

        //   List<Product> GetAllProducts();
    }
}
